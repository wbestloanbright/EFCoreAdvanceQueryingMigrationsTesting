using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Contacts.Controllers
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NamesValidationAttribute : ValidationAttribute, IClientModelValidator
    {

        public NamesValidationAttribute(string invalidName)
        {
            InvalidName = invalidName;
            ErrorMessage = string.Format("Name cannot be  equal to {0}.", InvalidName);
        }
        public string InvalidName { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ValidationResult returnValue = ValidationResult.Success;


            if (value != null && InvalidName != null)
            {
                if (value.ToString().ToUpper().Contains(InvalidName.ToUpper()))
                {
                    returnValue = new ValidationResult(ErrorMessage);
                }
            }

            return returnValue;
        }


        public void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-invalidname-compare-to-name"] = InvalidName;
            context.Attributes["data-val-invalidname"] = ErrorMessage;
        }
    }
}
