using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMeExam.Entities;
using MoneyMeExam.Repository;

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
        public IActionResult GetLoans() => Get<Loan>(nameof(GetLoans), DbContext.Loans.AsNoTracking());

        [HttpGet("{loanId}")]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult GetLoan([FromRoute] long? loanId) => Get<Loan>(nameof(GetLoans), DbContext.Loans.AsNoTracking(), loanId, (t => t.LoanId == loanId));

        [HttpPost]
        [ProducesResponseType(typeof(Loan), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateLoanAsync([FromBody] Loan loan) => await CreateAsync<Loan>(nameof(CreateLoanAsync), loan).ConfigureAwait(false);

        [HttpPut]
        [ProducesResponseType(typeof(Loan), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateLoanAsync([FromBody] Loan loan) => await UpdateAsync<Loan>(nameof(UpdateLoanAsync), (await DbContext.Loans.AsNoTracking().FirstOrDefaultAsync(t => t.LoanId == loan.LoanId).ConfigureAwait(false) is null), loan).ConfigureAwait(false);
    }
}
