namespace Application.Constants
{
    public class Regexes
    {
        public const string Email = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

        /// <summary>
        /// alpha numeric and underscore
        /// </summary>
        public const string Username = @"^[a-zA-Z0-9_]*$";

        /// <summary>
        /// may start with plus char, 11 digits max
        /// </summary>
        public const string PhoneNumber = @"^\+?\d{4,11}$";
    }
}
