using AutoMapper;
using CocShop.Core.Data.Entity;
using CocShop.Core.ViewModel;
using System;

namespace CocShop.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //AllowNullCollections = true;

            CreateMap<LoginViewModel, MyUser>().ReverseMap();

            CreateMap<RegisterViewModel, MyUser>().ReverseMap();

            CreateMap<Product, CreateProductRequestViewModel>().ReverseMap();

            CreateMap<ProductViewModel, Product>().ReverseMap();

            CreateMap<ProductCategoryViewModel, ProductCategory>().ReverseMap();

            CreateMap<ProductCategoryCreateRequest, ProductCategory>().ReverseMap();

            CreateMap<UpdateProductRequestViewModel, Product>().ReverseMap().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));


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
