using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using Application.Dto.User;
using AutoMapper;
using Infrastructure.Services;
using Presentation.ViewModels;
using Presentation.ViewModels.Order;
using Presentation.ViewModels.Product;
using Presentation.ViewModels.User;

namespace Presentation
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, TokenViewModel>();
            CreateMap<UserDto, UserViewModel>();
            CreateMap<RegisterViewModel, UserDto>()
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.Username.GetNormalized() ??
                                          source.Email.Replace('@', '_').Replace('.', '_').GetNormalized()))
                .ForMember(dest => dest.Email, opt =>
                    opt.MapFrom(source => source.Email.GetNormalized()));

            CreateMap<AddEditProductViewModel, ProductDto>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditOrderViewModel, OrderDto>();
            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();
            CreateMap<OrderDto, OrderViewModel>();
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}

