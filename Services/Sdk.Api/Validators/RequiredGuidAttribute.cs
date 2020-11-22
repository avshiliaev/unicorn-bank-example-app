using System;
using System.ComponentModel.DataAnnotations;

namespace Sdk.Api.Validators
{
    public class RequiredGuidAttribute : ValidationAttribute
    {
        public RequiredGuidAttribute()
        {
            ErrorMessage = "The {0} field is required.";
        }

        public override bool IsValid(object value)
        {
            var isGuidParsed = Guid.TryParse((string) value, out _);
            if (isGuidParsed)
                return true;

            return false;
        }
    }
}