namespace MoneyMeExam.Entities
{
    public class Loan : Interfaces.ILoan
    {
        public long? LoanId { get; set; }
        public long? ProductId { get; set; }
        public long? CustomerId { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? RepaymentTerms { get; set; }
        public decimal? InterestAmount { get; set; }
        public decimal? EstablishmentFee { get; set; }
        public decimal? TotalRepayments { get; set; }
        public Enums.RepaymentFrequency? RepaymentFrequency { get; set; }
        public Enums.LoanStatus? LoanStatus { get; set; }
        public virtual System.Collections.Generic.List<LoanDetail> LoanDetails { get; } = new System.Collections.Generic.List<LoanDetail>();
    }
}