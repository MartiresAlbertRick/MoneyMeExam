using Microsoft.VisualBasic;

namespace MoneyMeExam.ApiService.Services
{
    public class Calculator : ICalculator
    {
        public virtual double Compute(double rate, double terms, double amount)
        {
            return Financial.Pmt(rate, terms, amount);
        }
    }

    public class CalculateWithFirstTwoMonthsInterestFreeWithSixMonthDuration : Calculator
    {
        public override double Compute(double rate, double terms, double amount)
        {
            return base.Compute(rate, terms, amount);
        }
    }

    public interface ICalculator
    {
        double Compute(double rate, double terms, double amount);
    }
}
