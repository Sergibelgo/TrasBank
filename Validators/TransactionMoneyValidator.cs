using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Validators
{
    public class TransactionMoneyValidator : ValidationAttribute
    {
        public TransactionMoneyValidator() : base("The quantity should be divisor of 5")
        {

        }
        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            decimal val = (decimal)value;
            if (val % 5 != 0)
            {
                return new ValidationResult(
                        FormatErrorMessage(validationContext.DisplayName));
            }
            return ValidationResult.Success;
        }
    }
}
