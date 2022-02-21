using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class ProductValidator : BaseValidator<Product>
    {
        public ProductValidator() 
        {
            RuleFor(t => t.ProductName).NotNull().Length(0, 50);;
            RuleFor(t => t.ProductType).NotNull();
            RuleFor(t => t.InterestRate).NotNull();
        }
    }
}