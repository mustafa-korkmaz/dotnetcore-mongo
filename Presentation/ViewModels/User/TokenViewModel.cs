namespace Presentation.ViewModels.User
{
    public class TokenViewModel : UserViewModel
    {
        public string AccessToken { get; set; }
        public IReadOnlyCollection<string> Claims { get; set; } = Array.Empty<string>();
    }
}
