using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class EmailDomainValidator : BaseValidator<EmailDomain>
    {
        public EmailDomainValidator() 
        {
            RuleFor(t => t.EmailDomainName).NotNull().Length(0, 30);;
            RuleFor(t => t.IsBlackListed).NotNull().Must(value => value == false || value == true);
        }
    }
}