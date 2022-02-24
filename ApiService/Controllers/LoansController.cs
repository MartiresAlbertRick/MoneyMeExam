using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;
using Newtonsoft.Json;

namespace MoneyMeExam.ApiService.Controllers
{
    [ApiController]
    [Route("api/loans")]
    public class LoansController : MoneyMeBaseController
    {
        public LoansController(MoneyMeExamDbContext dbContext) : base(dbContext)
        { }

        [HttpGet]
        [ProducesResponseType(typeof(IQueryable<Loan>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetLoans() => Get<Loan>(nameof(GetLoans), DbContext.Loans.Include(i => i.LoanDetails).AsNoTracking());

        [HttpGet("{loanId}")]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetLoan([FromRoute] long? loanId) => Get<Loan>(nameof(GetLoans), DbContext.Loans.Include(i => i.LoanDetails).AsNoTracking(), loanId, (t => t.LoanId == loanId));

        [HttpPost]
        [ProducesResponseType(typeof(Loan), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateLoanAsync([FromBody] Loan loan) => await CreateAsync<Loan>(nameof(CreateLoanAsync), loan).ConfigureAwait(false);

        [HttpPost("third-party")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateLoanFromThirdPartyAsync([FromBody] LoanDTO loanDTO) 
        {
            Logger.Info(() => $"Received a request from third party to create loan application to {nameof(CreateLoanFromThirdPartyAsync)} with payload {JsonConvert.SerializeObject(loanDTO)}");
            IDbContextTransaction transaction = DbContext.Database.BeginTransaction();
            try
            {
                if (loanDTO is null) 
                {
                    return BadRequest(new { message = $"Invalid payload" });
                }
                Entities.Interfaces.ICustomer customer = loanDTO;
                Entities.Interfaces.ILoan loan = loanDTO;
                
                var newCustomer = new Customer 
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Title = customer.Title,
                    DateOfBirth = customer.DateOfBirth,
                    Mobile = customer.Mobile,
                    Email = customer.Email
                };

                await DbContext.AddAsync(newCustomer).ConfigureAwait(false);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                var newLoan = new Loan 
                {
                    ProductId = 1,
                    CustomerId = newCustomer.CustomerId,
                    LoanAmount = loan.LoanAmount,
                    InterestAmount = 0,
                    EstablishmentFee = 0,
                    TotalRepayments = 0,
                    RepaymentTerms = loan.RepaymentTerms,
                    RepaymentFrequency = Entities.Enums.RepaymentFrequency.Monthly,
                    LoanStatus = Entities.Enums.LoanStatus.InProgress
                };

                await DbContext.AddAsync(newLoan).ConfigureAwait(false);
                await DbContext.SaveChangesAsync().ConfigureAwait(false);

                await transaction.CommitAsync().ConfigureAwait(false);
                return StatusCode(201, new {
                    message = "Loan application successfully submitted",
                    redirectUrl = $"{(Request.IsHttps ? "https://" : "http://") + Request.Host.Value}/loan-application-step-1/{newLoan.LoanId}"
                });
            }
            catch (Exception e)
            {
                await RollbackTransactionAsync(transaction).ConfigureAwait(false);
                return ConvertExceptionToHttpStatusCode(e, nameof(CreateLoanFromThirdPartyAsync));
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateLoanAsync([FromBody] Loan loan) => await UpdateAsync<Loan>(nameof(UpdateLoanAsync), (await DbContext.Loans.AsNoTracking().FirstOrDefaultAsync(t => t.LoanId == loan.LoanId).ConfigureAwait(false) is null), loan, UpdateLoanDetailsAsync).ConfigureAwait(false);

        [HttpPut("compute-loan-repayments")]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ComputeLoanRepayments([FromBody] Loan loan)
        {
            try 
            {
                Product product = await DbContext.Products.AsNoTracking().FirstOrDefaultAsync(t => t.ProductId == loan.ProductId).ConfigureAwait(false);
                Services.ICalculator calculator;
                switch (product.ProductType)
                {
                    case Entities.Enums.ProductType.InterestFree:
                    case Entities.Enums.ProductType.NoInterestFree:
                        calculator = new Services.Calculator();
                        calculator.Compute(loan, product);  
                        break;
                    case Entities.Enums.ProductType.FirstTwoMonthsInterestFreeWithSixMonthDuration:
                        calculator = new Services.CalculateWithFirstTwoMonthsInterestFreeWithSixMonthDuration();
                        calculator.Compute(loan, product);
                        break;
                    default:
                        return BadRequest(new { message = "Invalid product type" });
                }
                return Ok(loan);    
            }
            catch (Exception e)
            {
                return ConvertExceptionToHttpStatusCode(e, nameof(ComputeLoanRepayments));
            }
        }

        [NonAction]
        public async Task UpdateLoanDetailsAsync(Loan loan) 
        {
            if (loan.LoanDetails.Count < 1) 
            {
                await DbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM loan_detail WHERE loan_id={loan.LoanId}").ConfigureAwait(false);
            }
            else 
            {
                Loan oldLoan = await DbContext.Loans.AsNoTracking().Include(t => t.LoanDetails).FirstOrDefaultAsync(t => t.LoanId == loan.LoanId);
                var oldDetailsForRemoval = new List<long>();
                foreach(LoanDetail detail in oldLoan.LoanDetails)
                {
                    if (loan.LoanDetails.Where(t => t.LoanDetailId == detail.LoanId).Count() < 1)
                    {
                        oldDetailsForRemoval.Add((long)detail.LoanDetailId);
                    }
                }
                if (oldDetailsForRemoval.Count > 0)
                {
                    await DbContext.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM loan_detail WHERE loan_detail_id IN ({oldDetailsForRemoval.ToArray()})").ConfigureAwait(false);
                }
            }
        }
    }
}
