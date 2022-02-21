namespace MoneyMeExam.Entities
{
    public class LoanDTO : Interfaces.ILoan, Interfaces.ICustomer
    {
        
        public decimal? LoanAmount { get; set; }
        public int? RepaymentTerms { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public System.DateTime? DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}