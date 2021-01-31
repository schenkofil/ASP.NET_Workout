using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Validation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
    public class UniqueCharactersAttribute : ValidationAttribute
    {
        private int numberOfSpecChars;
        public UniqueCharactersAttribute(int numberOfSpecChars)
        {
            this.numberOfSpecChars = numberOfSpecChars;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"{validationContext.MemberName} is a mandatory field.");
            }
            else if (value != null && value.ToString().Length > 0)
            {
                if (IsThisOkayBro(value.ToString()))
                {
                    return ValidationResult.Success;
                }
                else return new ValidationResult("Passwd doesn't contain 6 unique characters!", new List<string> { validationContext.MemberName });
            }
            throw new NotImplementedException($"SMOLA");
        }

        private bool IsThisOkayBro(string text)
        {
            if (text.Distinct().Count() >= 6) return true;
            return false;
        }
    }
}
