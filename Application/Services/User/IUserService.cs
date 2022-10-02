using Application.Dto.User;

namespace Application.Services.User
{
    public interface IUserService : IService<UserDto>
    {
        /// <summary>
        /// Checks for user by username or email. Sets user info and returns a valid token
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> GetTokenAsync(UserDto userDto, string password);
        
        Task RegisterAsync(UserDto userDto, string password);
    }
}
