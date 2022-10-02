using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.User
{
    public class GetTokenViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "EMAIL_OR_USERNAME")]
        public string? EmailOrUsername { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = 6)]
        [Display(Name = "PASSWORD")]
        public string? Password { get; set; }
    }
}
