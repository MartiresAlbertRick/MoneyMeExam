using System;

namespace MoneyMeExam.Entities.Interfaces
{
    public interface ICustomer
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Title { get; set; }
        DateTime? DateOfBirth { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
    }
}