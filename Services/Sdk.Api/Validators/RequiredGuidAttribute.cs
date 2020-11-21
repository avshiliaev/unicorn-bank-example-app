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
            if ((Guid) value != Guid.Empty)
                return true;

            return false;
        }
    }
}