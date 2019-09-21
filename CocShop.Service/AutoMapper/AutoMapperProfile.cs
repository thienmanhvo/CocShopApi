using AutoMapper;
using CocShop.Data.Entity;
using CocShop.Service.ViewModel;
using System;

namespace CocShop.Service.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LoginViewModel, MyUser>().ReverseMap();

            CreateMap<RegisterViewModel, MyUser>().ReverseMap();

            CreateMap<Product, ProductRequestViewModel>().ReverseMap();

            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<ProductCategoryViewModel, ProductCategory>().ReverseMap();

            CreateMap<ProductCategoryCreateRequest, ProductCategory>().ReverseMap();

            
            //CreateMap<string, Guid>().ConvertUsing(new StringToGuidConverter());
            //CreateMap<Guid, string>().ConvertUsing(new GuidToStringConverter());
        }
    }
    public class GuidToStringConverter : ITypeConverter<Guid, string>
    {
        public string Convert(Guid source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
    public class StringToGuidConverter : ITypeConverter<string, Guid>
    {
        public Guid Convert(string source, Guid destination, ResolutionContext context)
        {
            return Guid.Parse(source);
        }
    }
}
