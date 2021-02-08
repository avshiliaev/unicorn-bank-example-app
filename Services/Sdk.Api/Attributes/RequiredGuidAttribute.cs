using System;
using System.ComponentModel.DataAnnotations;

namespace Sdk.Api.Attributes
{
    public class IsValidGuidAttribute : ValidationAttribute
    {
        public IsValidGuidAttribute()
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
}