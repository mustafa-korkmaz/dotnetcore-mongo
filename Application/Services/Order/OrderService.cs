﻿
using Application.Constants;
using Application.Dto.Order;
using Application.Exceptions;
using AutoMapper;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Order
{
    public class OrderService : ServiceBase<IOrderRepository, Domain.Aggregates.Order.Order, OrderDto>, IOrderService
    {
        private readonly IProductRepository _productRepository;

        public OrderService(IUnitOfWork uow, ILogger<OrderService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _productRepository = Uow.GetRepository<IProductRepository, Domain.Aggregates.Product.Product>();
        }

        public override async Task AddAsync(OrderDto dto)
        {
            var document = Mapper.Map<OrderDto, Domain.Aggregates.Order.Order>(dto);

            var productIds = document.Items.Select(x => x.ProductId).ToArray();

            var products = await _productRepository.ListByIdsAsync(productIds);

            if (productIds.Length != products.Count)
            {
                throw new ValidationException(ErrorMessages.ProductsNotFound);
            }

            //requires transactional operation

            foreach (var item in products)
            {
                var orderItem = document.Items.First(p => p.ProductId == item.Id);

                item.RemoveStock(orderItem.Quantity);

                if (item.StockQuantity < 0)
                {
                    throw new ValidationException(ErrorMessages.InsufficientStock);
                }

                await _productRepository.ReplaceOneAsync(item);
            }

            //set new members
            dto.Id = document.Id;
            dto.CreatedAt = document.CreatedAt;

            await Repository.InsertOneAsync(document);
        }
    }
}