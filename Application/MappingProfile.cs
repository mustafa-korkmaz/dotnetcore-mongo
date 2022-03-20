using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using MongoDB.Bson;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DtoBase, Document>()
                .ConstructUsing(src => new Document(src.Id))
                .ReverseMap();

            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));

            CreateMap<ProductDto, Product>()
              .IncludeBase<DtoBase, Document>()
              .ConstructUsing(src => new Product(ObjectId.GenerateNewId().ToString(), src.Sku, src.Name, src.UnitPrice))
              .ReverseMap();

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConstructUsing(src => new Order(ObjectId.GenerateNewId().ToString(), src.Username))
                .AfterMap((src, dest) =>
                {
                    foreach (var item in src.Items)
                    {
                        dest.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
                    }
                });
        }
    }
}
