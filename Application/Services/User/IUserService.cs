using Application.Dto;
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

        /// <summary>
        /// search by email or name surname
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        Task<ListDtoResponse<UserDto>> SearchAsync(int offset, int limit, string? searchText);

        Task RegisterAsync(UserDto userDto, string password);

        Task ApproveAsync(string id);

        Task RejectAsync(string id);
    }
}
