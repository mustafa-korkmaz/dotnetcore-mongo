namespace Domain.Aggregates.User
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByUsernameAsync(string username);
    }
}
