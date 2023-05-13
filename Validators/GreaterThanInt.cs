
using System.ComponentModel.DataAnnotations;

namespace Validators
{
    public class GreaterThanInt : ValidationAttribute
    {
        public GreaterThanInt(int number) : base("{0} must be greater than {1}")
        {
            Number = number;
        }
        public int Number { get; set; }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, Number);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var firstComparable = value as IComparable;
            var secondComparable = Number as IComparable;
            if (firstComparable is not null && secondComparable is not null)
            {
                if (firstComparable.CompareTo(secondComparable)>-1)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName)); ;
        }
    }
}
