using MoneyMeExam.Entities;

namespace MoneyMeExam.ApiService.Services
{
    public class ComputeLoanRepayments
    {
        public virtual Loan Compute()
        {
            return new Loan();
        }
    }

    public class ComputeLoanRepaymentWithNoInterestFree : ComputeLoanRepayments
    {
        public override Loan Compute()
        {
            return base.Compute();
        }
    }

    public class ComputeLoanRepaymentWithInterestFree : ComputeLoanRepayments
    {
        public override Loan Compute()
        {
            return base.Compute();
        }
    }

    public class ComputeLoanRepaymentWithFirstTwoMonthsInterestFreeWithSixMonthDuration : ComputeLoanRepayments
    {
        public override Loan Compute()
        {
            return base.Compute();
        }
    }
}
