namespace MoneyMeExam.Entities.Interfaces
{
    public interface ILoan
    {
        decimal? LoanAmount { get; set; }
        int? RepaymentTerms { get; set; }
    }
}