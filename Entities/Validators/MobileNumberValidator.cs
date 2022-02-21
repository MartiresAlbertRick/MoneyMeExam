using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class MobileNumberValidator : BaseValidator<MobileNumber>
    {
        public MobileNumberValidator() 
        {
            RuleFor(t => t.Number).NotNull().Length(0, 30);;
            RuleFor(t => t.IsBlackListed).NotNull().Must(value => value == false || value == true);
        }
    }
}