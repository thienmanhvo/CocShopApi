using AutoMapper;
using CocShop.Core.Data.Entity;
using CocShop.Core.ViewModel;
using System;
using System.Globalization;

namespace CocShop.Core.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //AllowNullCollections = true;

            CreateMap<LoginViewModel, MyUser>().ReverseMap();
            CreateMap<RegisterViewModel, MyUser>().ReverseMap();

            CreateMap<DateTime, string>().ConvertUsing(new DatetimeToStringConverter());
            CreateMap<string, DateTime>().ConvertUsing(new StringToDatetimeConverter());

            CreateMap<Product, CreateProductRequestViewModel>().ReverseMap();
            CreateMap<ProductViewModel, Product>().ReverseMap();
            CreateMap<UpdateProductRequestViewModel, Product>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<Product, UpdateProductRequestViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<ProductCategoryViewModel, ProductCategory>().ReverseMap();
            CreateMap<CreateProductCategoryRequestViewModel, ProductCategory>().ReverseMap();
            CreateMap<ProductCategory, UpdateProductCategoryViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<UpdateProductCategoryViewModel, ProductCategory>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<OrderViewModel, Order>().ReverseMap();
            CreateMap<Order, CreateOrderRequestViewModel>().ReverseMap();
            //CreateMap<Order, UpdateOrderRequestViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //CreateMap<UpdateOrderRequestViewModel, Order>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<OrderDetailViewModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail, CreateOrderDetailViewModel>().ReverseMap();
            CreateMap<ProductToOrderViewModel, OrderDetail>().ReverseMap();

            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<Location, CreateLocationRequestViewModel>().ReverseMap();
            CreateMap<UpdateLocationRequestViewModel, Location>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<Location, UpdateLocationRequestViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<PaymentMethod, PaymentMethodViewModel>().ReverseMap();
            CreateMap<PaymentMethod, CreatePaymentMethodRequestViewModel>().ReverseMap();
            CreateMap<UpdatePaymentMethodRequestViewModel, PaymentMethod>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<PaymentMethod, UpdatePaymentMethodRequestViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<MyUser, MyUserViewModel>().ReverseMap();
            //CreateMap<PaymentMethod, CreatePaymentMethodRequestViewModel>().ReverseMap();
            CreateMap<UpdateMyUserRequestViewModel, MyUser>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            CreateMap<MyUser, UpdateMyUserRequestViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<MyRole, RoleViewModel>().ReverseMap();
            CreateMap<MyRole, CreateRoleRequestViewModel>().ReverseMap();
            //        CreateMap<OrderDetail, UpdateOrderDetailViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //        CreateMap<UpdateOrderDetailViewModel, OrderDetail>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            //CreateMap<string, Guid>().ConvertUsing(new StringToGuidConverter());
            //CreateMap<Guid, string>().ConvertUsing(new GuidToStringConverter());


            CreateMap<StoreViewModel, Store>().ReverseMap();
            //CreateMap<CreateProductCategoryRequestViewModel, ProductCategory>().ReverseMap();
            //CreateMap<ProductCategory, UpdateProductCategoryViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //CreateMap<UpdateProductCategoryViewModel, ProductCategory>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));  

            CreateMap<BrandViewModel, Brand>().ReverseMap();
            //CreateMap<CreateProductCategoryRequestViewModel, ProductCategory>().ReverseMap();
            //CreateMap<ProductCategory, UpdateProductCategoryViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //CreateMap<UpdateProductCategoryViewModel, ProductCategory>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));  

            CreateMap<StoreCategoryViewModel, StoreCategory>().ReverseMap();
            //CreateMap<CreateProductCategoryRequestViewModel, ProductCategory>().ReverseMap();
            //CreateMap<ProductCategory, UpdateProductCategoryViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //CreateMap<UpdateProductCategoryViewModel, ProductCategory>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

            CreateMap<MenuDish, MenuDishViewModel>().ReverseMap();
            //CreateMap<CreateProductCategoryRequestViewModel, ProductCategory>().ReverseMap();
            //CreateMap<ProductCategory, UpdateProductCategoryViewModel>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));
            //CreateMap<UpdateProductCategoryViewModel, ProductCategory>().ForAllMembers(opt => opt.Condition((source, dest, sourceMember, destMember) => (sourceMember != null)));

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
    public class DatetimeToStringConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime source, string destination, ResolutionContext context)
        {
            return source.ToString("yyyyMMdd");
        }
    }
    public class StringToDatetimeConverter : ITypeConverter<string, DateTime>
    {
        public DateTime Convert(string source, DateTime destination, ResolutionContext context)
        {
            return DateTime.ParseExact(source, "yyyyMMdd", CultureInfo.InvariantCulture);
        }
    }
}
