
using System.ComponentModel.DataAnnotations;


namespace Validators
{
    public class LoanTypeValidator : ValidationAttribute
    {
        public LoanTypeValidator() : base("Loan type must be 1 or 2")
        {


        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString);
        }

        protected override ValidationResult
            IsValid(object firstValue, ValidationContext validationContext)
        {
            IComparable firstComparable = firstValue as IComparable;

            if (firstComparable != null)
            {
                if (firstComparable.CompareTo(1) != 0 && firstComparable.CompareTo(2) != 0)
                {
                    return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
                }
            }
            return ValidationResult.Success;
        }
    }
}
