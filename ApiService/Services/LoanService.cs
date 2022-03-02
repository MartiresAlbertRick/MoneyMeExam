using Microsoft.VisualBasic;
using System;
using System.Linq;

namespace MoneyMeExam.ApiService.Services
{
    public class Calculator : ICalculator
    {
        public virtual void Compute(Entities.Loan loan, Entities.Product product)
        {
            decimal monthlyPayments = (product.ProductType == Entities.Enums.ProductType.InterestFree) ? Math.Round((decimal)loan.LoanAmount / (int)loan.RepaymentTerms, 2) : Convert.ToDecimal(Math.Round(Math.Abs(Financial.Pmt(Convert.ToDouble(product.InterestRate), Convert.ToDouble(loan.RepaymentTerms), Convert.ToDouble(loan.LoanAmount))), 2));
            decimal interestAmount = (monthlyPayments * (int)loan.RepaymentTerms) - (decimal)loan.LoanAmount;
            loan.EstablishmentFee = product.EstablishmentFee;
            loan.TotalRepayments = (monthlyPayments * loan.RepaymentTerms) + product.EstablishmentFee;
            monthlyPayments = (decimal)loan.TotalRepayments / (int)loan.RepaymentTerms;
            loan.InterestAmount = interestAmount < 0 ? 0 : interestAmount;
            loan.LoanDetails.Clear();
            DateTime dueDate = DateTime.Now;
            for (int i = 0; i < loan.RepaymentTerms; i++)
            {
                dueDate = dueDate.AddMonths(1);
                loan.LoanDetails.Add(new Entities.LoanDetail{
                    LoanId = loan.LoanId,
                    Amount = monthlyPayments,
                    DueDate = dueDate
                });
            }
        }
    }

    public class CalculateWithFirstTwoMonthsInterestFreeWithSixMonthDuration : Calculator
    {
        public override void Compute(Entities.Loan loan, Entities.Product product)
        {
            base.Compute(loan, product);
            if (loan.RepaymentTerms >= 6)
            {
                //compute monthly payments with interest free
                decimal monthlyPayments = Math.Round((decimal)loan.LoanAmount / (int)loan.RepaymentTerms, 2);
                decimal totalRepayments = (monthlyPayments * (int)loan.RepaymentTerms) + (decimal)product.EstablishmentFee;
                monthlyPayments = totalRepayments / (int)loan.RepaymentTerms;
                IQueryable<Entities.LoanDetail> firstTwoMonths = loan.LoanDetails.OrderBy(t => t.DueDate).Take(2).AsQueryable();
                foreach(Entities.LoanDetail loanDetail in firstTwoMonths)
                {
                    loan.TotalRepayments -= (loanDetail.Amount - monthlyPayments);
                    loanDetail.Amount = monthlyPayments;
                }
                loan.InterestAmount = (loan.TotalRepayments - loan.LoanAmount) - loan.EstablishmentFee;
            }
        }
    }

    public interface ICalculator
    {
        void Compute(Entities.Loan loan, Entities.Product product);
    }
}
