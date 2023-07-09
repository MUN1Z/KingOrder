using AutoMapper;
using VamoPlay.Application.ViewModels.Response;
using VamoPlay.Domain.Entities;

namespace VamoPlay.Application.AutoMapperProfiles
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
