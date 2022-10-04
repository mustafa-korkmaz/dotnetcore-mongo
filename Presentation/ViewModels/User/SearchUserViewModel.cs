using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.User
{
    public class SearchUserViewModel : ListViewModelRequest
    {
        [StringLength(100, ErrorMessage = ValidationErrorCode.BetweenLength, MinimumLength = AppConstants.MinimumLengthForSearch)]
        [Display(Name = "SEARCH_TEXT")]
        public string? SearchText { get; set; }
    }
}
