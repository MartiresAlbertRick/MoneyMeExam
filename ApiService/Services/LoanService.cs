using Microsoft.VisualBasic;
using System;

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
        }
    }

    public interface ICalculator
    {
        void Compute(Entities.Loan loan, Entities.Product product);
    }
}
