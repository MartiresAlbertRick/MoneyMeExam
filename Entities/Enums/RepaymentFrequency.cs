using System.ComponentModel;

namespace MoneyMeExam.Entities.Enums
{
    public enum RepaymentFrequency
    {
        [Description("Daily based repayments")]
        Daily,
        [Description("Weekly based repayments")]
        Weekly,
        [Description("Monthly based repayments")]
        Monthly,
        [Description("Quarterly based repayments")]
        Quarterly,
    }
}