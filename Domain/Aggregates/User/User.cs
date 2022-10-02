
namespace Domain.Aggregates.User
{
    public class User : Document
    {
        public string Username { get; private set; }

        public string? NameSurname { get; private set; }

        public string Email { get; private set; }

        public string? PhoneNumber { get; private set; }

        public string PasswordHash { get; private set; }

        public bool IsEmailConfirmed { get; private set; }

        private ICollection<string>? _claims;
        public IReadOnlyCollection<string>? Claims
        {
            get => _claims?.ToList();
            private set
            {
                // mongo db serialization will use this part
                if (value == null)
                {
                    _claims = new List<string>();
                }
                else
                    _claims = value.ToList();
            }
        }

        public User(string id, string username, string? nameSurname, string email, string? phoneNumber,
            string passwordHash, bool isEmailConfirmed) : base(id)
        {
            Username = username;
            NameSurname = nameSurname;
            Email = email;
            PhoneNumber = phoneNumber;
            PasswordHash = passwordHash;
            IsEmailConfirmed = isEmailConfirmed;
        }

        public void AddClaim(string claim)
        {
            if (_claims == null)
            {
                _claims = new List<string>();
            }

            _claims.Add(claim);
        }

        public void SetIsEmailConfirmed(bool value)
        {
            IsEmailConfirmed = value;
        }

        public void SetProfile(string nameSurname, string phoneNumber)
        {
            NameSurname = nameSurname;
            PhoneNumber = phoneNumber;
        }

        public void SetPasswordHash(string passwordHash)
        {
            PasswordHash = passwordHash;
        }
    }
}
