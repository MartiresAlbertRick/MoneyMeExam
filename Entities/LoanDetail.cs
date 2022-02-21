namespace MoneyMeExam.Entities
{
    public class LoanDetail
    {
        public long? LoanDetailId { get; set; }
        public long? LoanId { get; set; }
        public decimal? Amount { get; set; }
        public System.DateTime? DueDate { get; set; }
    }
}