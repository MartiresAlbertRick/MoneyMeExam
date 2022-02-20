namespace MoneyMeExam.Entities.Interfaces
{
    public interface ILoan
    {
        long? LoanId { get; set; }
        long? ProductId { get; set; }
        long? CustomerId { get; set; }
        decimal? LoanAmount { get; set; }
        int? RepaymentTerms { get; set; }
        Enums.RepaymentFrequency? RepaymentFrequency { get; set; }
        Enums.LoanStatus? LoanStatus { get; set; }
    }
}