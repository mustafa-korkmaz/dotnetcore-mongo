using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using Application.Dto.User;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
using Domain.Aggregates.User;
using MongoDB.Bson;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Document, DtoBase>();
            CreateMap<DtoBase, Document>()
                .ConvertUsing(src => new Document(src.Id));

            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
              .ConvertUsing(src => new Product(ObjectId.GenerateNewId().ToString(), src.Sku, src.Name, src.UnitPrice, src.StockQuantity));

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ConvertUsing((src, _) =>
                {
                    var user = new User(ObjectId.GenerateNewId().ToString(), src.Username, src.NameSurname, src.Email,
                        src.PhoneNumber, "", false);

                    if (src.Claims != null)
                    {
                        foreach (var item in src.Claims)
                        {
                            user.AddClaim(item);
                        }
                    }

                    return user;
                });

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, _) =>
                {
                    var order = new Order(ObjectId.GenerateNewId().ToString(), src.Username);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
                    }

                    return order;
                });
        }
    }
}
