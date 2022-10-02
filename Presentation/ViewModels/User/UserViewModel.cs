namespace Presentation.ViewModels.User
{
    public class UserViewModel : ViewModelBase
    {
        public string Username { get; set; }

        public string? NameSurname { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }
    }
}
