
using Application.Dto.Order;
using AutoMapper;
using Domain.Aggregates.Order;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Order
{
    public class OrderService : ServiceBase<IOrderRepository, Domain.Aggregates.Order.Order, OrderDto>, IOrderService
    {
        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {

        }
    }
}