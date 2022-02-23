using Microsoft.VisualBasic;
using MoneyMeExam.Entities;

namespace MoneyMeExam.ApiService.Services
{
    public abstract class Calculator
    {
        public abstract double Compute();
    }

    public class CalculateWithNoInterestFree : Calculator
    {
        public override double Compute();
    }

    public class CalculateWithFirstTwoMonthsInterestFreeWithSixMonthDuration : Calculator
    {
        // public override double Compute(double loanAmount, double paymentTerms = 1, double rate = 0)
        // {
        //     return base.Compute(rate, paymentTerms, loanAmount);
        // }
        public override double Compute();
    }

    public class MainCalculator<T> where T : Calculator
    {
        public double Rate {get; set; }
        public double PaymentTerms { get; set; }
        public double LoanAmount { get; set; }

        public double Compute()
        {
            return Financial.Pmt(Rate, PaymentTerms, LoanAmount);
        }
    }
}
