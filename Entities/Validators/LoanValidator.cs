using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class LoanValidator : BaseValidator<Loan>
    {
        public LoanValidator() 
        {
            RuleFor(t => t.ProductId).NotNull();
            RuleFor(t => t.CustomerId).NotNull().Must(BeGreaterThanZero);
            RuleFor(t => t.LoanAmount).NotNull().Must(BeGreaterThanZero);
            RuleFor(t => t.RepaymentTerms).NotNull().Must(BeGreaterThanZero);
            RuleFor(t => t.RepaymentFrequency).NotNull();
            RuleFor(t => t.LoanStatus).NotNull();
        }
    }
}