using Domain.Aggregates.Order;
using Infrastructure.Persistance.MongoDb;

namespace Infrastructure.Repositories
{
    internal class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IMongoContext context) : base(context)
        {

        }
    }
}
