using FluentValidation;

namespace MoneyMeExam.Entities.Validators
{
    public class BaseValidator<T> : AbstractValidator<T>
    {
        public bool BeGreaterThanZero<TValue>(TValue value)
        {
            if (System.Convert.ToDecimal(value) > 0)
            {
                return true;
            }
            return false;
        }
    }
}