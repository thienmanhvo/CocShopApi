using AutoMapper;
using CocShop.Data.Entity;
using CocShop.Service.ViewModel;

namespace CocShop.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginViewModel, MyUser>();
            CreateMap<MyUser, LoginViewModel>();
            
            CreateMap<RegisterViewModel, MyUser>();
            CreateMap<MyUser, RegisterViewModel>();

            CreateMap<Product, ProductRequestViewModel>();
            CreateMap<ProductRequestViewModel, Product>();

            CreateMap<ProductViewModel, ProductRequestViewModel>();
            CreateMap<ProductRequestViewModel, ProductViewModel>();
        }
    }
}
