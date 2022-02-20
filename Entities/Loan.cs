using System;

namespace MoneyMeExam.Entities
{
    public class Loan : Interfaces.ILoan
    {
        public long? LoanId { get; set; }
        public long? ProductId { get; set; }
        public long? CustomerId { get; set; }
        public decimal? LoanAmount { get; set; }
        public int? RepaymentTerms { get; set; }
        public Enums.RepaymentFrequency? RepaymentFrequency { get; set; }
        public Enums.LoanStatus? LoanStatus { get; set; }
    }
}