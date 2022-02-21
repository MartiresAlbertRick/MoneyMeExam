using System.ComponentModel;

namespace MoneyMeExam.Entities.Enums
{
    public enum ProductType
    {
        [Description("No interest-free")]
        NoInterestFree,
        [Description("Loan is interest-free")]
        InterestFree,
        [Description("First 2 months are interest-free with the minimum duration of 6 months")]
        FirstTwoMonthsInterestFreeWithSixMonthDuration
    }
}