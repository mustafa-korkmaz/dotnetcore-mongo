namespace Application.Dto.User
{
    public class UserDto : DtoBase
    {
        public string Username { get; set; }

        public string? NameSurname { get; set; }

        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public ICollection<string> Claims { get; set; }
    }
}
