namespace MoneyMeExam.Entities.Interfaces
{
    public interface ICustomer
    {
        long? CustomerId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Title { get; set; }
        System.DateTime? DateOfBirth { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
    }
}