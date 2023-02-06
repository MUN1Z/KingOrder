using AutoMapper;
using KingOrder.Application.Shared.ViewModels.Response;
using KingOrder.Domain.Entities;

namespace KingOrder.Application.AutoMapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestViewModel, Product>();
            CreateMap<ProductResponseViewModel, Product>();

            CreateMap<Product, ProductRequestViewModel>();
            CreateMap<Product, ProductResponseViewModel>();

            CreateMap<ProductRequestViewModel, ProductResponseViewModel>();
            CreateMap<ProductResponseViewModel, ProductRequestViewModel>();
        }
    }
}
