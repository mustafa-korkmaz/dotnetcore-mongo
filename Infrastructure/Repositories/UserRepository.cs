using Domain.Aggregates.User;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    internal class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {

        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);

            return await Collection.Find(filter).SingleOrDefaultAsync();
        }
    }
}
