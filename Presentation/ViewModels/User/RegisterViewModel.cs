using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.User
{
    public class RegisterViewModel
    {
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [RegularExpression(Regexes.Username, ErrorMessage = ValidationErrorCode.UsernameNotValid)]
        [Display(Name = "USERNAME")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 4)]
        [Display(Name = "NAME_SURNAME")]
        public string? NameSurname { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [RegularExpression(Regexes.Email, ErrorMessage = ValidationErrorCode.EmailNotValid)]
        [Display(Name = "EMAIL")]
        public string Email { get; set; }

        [RegularExpression(Regexes.PhoneNumber, ErrorMessage = ValidationErrorCode.PhoneNumberNotValid)]
        [Display(Name = "PHONE_NUMBER")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 6)]
        [Display(Name = "PASSWORD")]
        public string? Password { get; set; }
    }
}
