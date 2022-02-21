using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class LoanDTOValidator : BaseValidator<LoanDTO>
    {
        public LoanDTOValidator() 
        {
            RuleFor(t => t.LoanAmount).NotNull().Must(BeGreaterThanZero);
            RuleFor(t => t.RepaymentTerms).NotNull().Must(BeGreaterThanZero);
            RuleFor(t => t.FirstName).NotNull().Length(0, 30);
            RuleFor(t => t.LastName).NotNull().Length(0, 30);
            RuleFor(t => t.Title).Length(0, 30);
            RuleFor(t => t.DateOfBirth).NotNull();
            RuleFor(t => t.Mobile).Length(0, 30);
            RuleFor(t => t.Email).EmailAddress().Length(0, 30);
        }
    }
}