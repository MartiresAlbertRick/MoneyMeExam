using System;

namespace MoneyMeExam.Entities
{
    public class Customer : Interfaces.ICustomer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
    }
}