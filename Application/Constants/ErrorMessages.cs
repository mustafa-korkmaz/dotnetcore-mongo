using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constants
{
    public static class ErrorMessages
    {
        public const string ProductsNotFound = "All products cannot be found";
        
        public const string InsufficientStocks = "Insufficient stock quantity";
       
        public const string UserNotFound = "USER_NOT_FOUND";
      
        public const string RecordNotFound = "RECORD_NOT_FOUND";
     
        public const string InternalServerError = "UNEXPECTED_ERROR";
     
        public const string IncorrectUsernameOrPassword = "USERNAME_OR_PASSWORD_INCORRECT";
       
        public const string UserExists = "USER_ALREADY_EXISTS";
       
        public const string EmailExists = "EMAIL_ALREADY_EXISTS";
    }

    public static class ValidationErrorCode
    {
        public const string RequiredField = "{0}_FIELD_IS_REQUIRED";

        public const string MaxLength = "{0}_FIELD_SHOULD_BE_MAX_{1}_CHARS";

        public const string GreaterThan = "{0}_FIELD_SHOULD_BE_GREATER_THAN_{1}";

        public const string BetweenLength = "{0}_FIELD_SHOULD_BE_MIN_{2}_MAX_{1}_CHARS";

        public const string ExactLength = "{0}_FIELD_SHOULD_BE_{1}_CHARS";

        public const string BetweenRange = "{0}_FIELD_SHOULD_BE_BETWEEN_{1}_AND_{2}";

        public const string EmailNotValid = "EMAIL_FIELD_IS_NOT_VALID";

        public const string UrlNotValid = "URL_FIELD_IS_NOT_VALID";

        public const string DateNotValid = "DATE_FORMAT_IS_NOT_VALID";

        public const string UsernameNotValid = "USERNAME_IS_NOT_VALID";

        public const string PhoneNumberNotValid = "PHONE_NUMBER_IS_NOT_VALID";

    }
}
