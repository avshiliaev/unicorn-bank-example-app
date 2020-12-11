using System;
using System.ComponentModel.DataAnnotations;

namespace Sdk.Api.Validators
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute()
        {
            ErrorMessage = "The {0} field is invalid.";
        }

        public override bool IsValid(object value)
        {
            var isGuidParsed = Guid.TryParse((string) value, out _);
            if (isGuidParsed)
                return true;

            return false;
        }
    }
    
    public class RoleBasedAttribute : ValidationAttribute
    {
        private string _scope;

        public RoleBasedAttribute(string scope)
        {
            _scope = scope;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            
            return ValidationResult.Success;
        }
    }
}