using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.User
{
    public class StatusViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "VALUE")]
        public bool? Value { get; set; }
    }
}
