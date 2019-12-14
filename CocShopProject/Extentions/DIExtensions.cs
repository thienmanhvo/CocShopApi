using AutoMapper;
using CocShop.Core.AutoMapper;
using CocShop.Core.Data.Infrastructure;
using CocShop.Core.Data.Repository;
using CocShop.Core.Service;
using CocShop.Repository.Repositories;
using CocShop.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CocShop.WebAPi.Extentions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDI(this IServiceCollection services)
        {
            AddServiceDI(services);
            AddRepoistoryDI(services);
            //add for data
            services.AddScoped<IDbFactory, DbFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            //EventLog


            //SignalR
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region automapper
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion
            return services;
        }

        public static void AddServiceDI(IServiceCollection services)
        {
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IPaymentMethodService, PaymentMethodService>();
            services.AddTransient<IMyUserService, MyUserService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IStoreCategoryRepository, StoreCategoryRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
        }

        public static void AddRepoistoryDI(IServiceCollection services)
        {
            services.AddTransient<IHubUserConnectionService, HubUserConnectionService>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
            services.AddTransient<IOrderDetailRepository, OrderDetailRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<ILocationRepository, LocationRepository>();
            services.AddTransient<IPaymentMethodRepository, PaymentMethodRepository>();
            services.AddTransient<IMyUserRepository, MyUserRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();

        }

    }
}
