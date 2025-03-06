using ECommerceSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Persistence.Repositories;



namespace ECommerceSystem.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<CommonTimeInterceptor>();

            services.AddDbContext<ECommerceDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<CommonTimeInterceptor>();
                options.UseNpgsql(Configuration.ConnectionString)
                       .AddInterceptors(interceptor);
            });
            services.AddScoped<IAddressReadRepository, AddressReadRepository>();
            services.AddScoped<IAddressWriteRepository, AddressWriteRepository>();
            services.AddScoped<IAuditLogReadRepository, AuditLogReadRepository>();
            services.AddScoped<IAuditLogWriteRepository, AuditLogWriteRepository>();
            services.AddScoped<ICartReadRepository, CartReadRepository>();
            services.AddScoped<ICartWriteRepository, CartWriteRepository>();
            services.AddScoped<ICartProductReadRepository, CartProductReadRepository>();
            services.AddScoped<ICartProductWriteRepository, CartProductWriteRepository>();
            services.AddScoped<IProductWarehouseReadRepository, ProductWarehouseReadRepository>();
            services.AddScoped<IProductWarehouseWriteRepository, ProductWarehouseWriteRepository>();
            services.AddScoped<IStockTransactionReadRepository, StockTransactionReadRepository>();
            services.AddScoped<IStockTransactionWriteRepository, StockTransactionWriteRepository>();
            services.AddScoped<IWarehouseReadRepository, WarehouseReadRepository>();
            services.AddScoped<IWarehouseWriteRepository, WarehouseWriteRepository>();
            services.AddScoped<IOrderReturnReadRepository, OrderReturnReadRepository>();
            services.AddScoped<IOrderReturnWriteRepository, OrderReturnWriteRepository>();
            services.AddScoped<IOrderReturnItemReadRepository, OrderReturnItemReadRepository>();
            services.AddScoped<IOrderReturnItemWriteRepository, OrderReturnItemWriteRepository>();
            services.AddScoped<ICarrierReadRepository, CarrierReadRepository>();
            services.AddScoped<ICarrierWriteRepository, CarrierWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IOrderProductReadRepository, OrderProductReadRepository>();
            services.AddScoped<IOrderProductWriteRepository, OrderProductWriteRepository>();
            services.AddScoped<ITrackingDetailReadRepository, TrackingDetailReadRepository>();
            services.AddScoped<ITrackingDetailWriteRepository, TrackingDetailWriteRepository>();
            services.AddScoped<IPaymentReadRepository, PaymentReadRepository>();
            services.AddScoped<IPaymentWriteRepository, PaymentWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IProductAttributeReadRepository, ProductAttributeReadRepository>();
            services.AddScoped<IProductAttributeWriteRepository, ProductAttributeWriteRepository>();
            services.AddScoped<IProductAttributeValueReadRepository, ProductAttributeValueReadRepository>();
            services.AddScoped<IProductAttributeValueWriteRepository, ProductAttributeValueWriteRepository>();
            services.AddScoped<IProductCategoryReadRepository, ProductCategoryReadRepository>();
            services.AddScoped<IProductCategoryWriteRepository, ProductCategoryWriteRepository>();
            services.AddScoped<IProductImageReadRepository, ProductImageReadRepository>();
            services.AddScoped<IProductImageWriteRepository, ProductImageWriteRepository>();
            services.AddScoped<IProductProductCategoryReadRepository, ProductProductCategoryReadRepository>();
            services.AddScoped<IProductProductCategoryWriteRepository, ProductProductCategoryWriteRepository>();
            services.AddScoped<IProductReviewReadRepository, ProductReviewReadRepository>();
            services.AddScoped<IProductReviewWriteRepository, ProductReviewWriteRepository>();
            services.AddScoped<IProductVariationReadRepository, ProductVariationReadRepository>();
            services.AddScoped<IProductVariationWriteRepository, ProductVariationWriteRepository>();
            services.AddScoped<IProductVariationAttributeReadRepository, ProductVariationAttributeReadRepository>();
            services.AddScoped<IProductVariationAttributeWriteRepository, ProductVariationAttributeWriteRepository>();
            services.AddScoped<IPromotionReadRepository, PromotionReadRepository>();
            services.AddScoped<IPromotionWriteRepository, PromotionWriteRepository>();
            services.AddScoped<IPromotionCategoryReadRepository, PromotionCategoryReadRepository>();
            services.AddScoped<IPromotionCategoryWriteRepository, PromotionCategoryWriteRepository>();
            services.AddScoped<IPromotionProductReadRepository, PromotionProductReadRepository>();
            services.AddScoped<IPromotionProductWriteRepository, PromotionProductWriteRepository>();
            services.AddScoped<IRoleReadRepository, RoleReadRepository>();
            services.AddScoped<IRoleWriteRepository, RoleWriteRepository>();
            services.AddScoped<IUserReadRepository, UserReadRepository>();
            services.AddScoped<IUserWriteRepository, UserWriteRepository>();

            
        }
    }
}
