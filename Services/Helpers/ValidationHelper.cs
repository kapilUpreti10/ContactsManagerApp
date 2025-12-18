using ServiceContracts.dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helpers
{
    public class ValidationHelper
    {
        // this type of validation helper is useful when want to validate any dto object before processing it ie before adding or updating it in the db 

       public static void ValidateModelProperties(object? obj)
        {

            // since it takes obj as arg so it can validate any object passed to it and properties can be anything 

            ValidationContext validationContext = new ValidationContext(obj);

            // here this list is to collect all the validation results either error or success
            List<ValidationResult> validationREsults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationREsults, true);
            // here if we dont give true then it only validate the properties which have validation attribute required but since we want to validate all properties which dont have req also


            if (!isValid)
            {
                // collect all error messages from validationresults list
                string errorMessages = string.Join("; ", validationREsults.Select(result => result.ErrorMessage));
                throw new ArgumentException(errorMessages);
            }
        }
    
    }
}

