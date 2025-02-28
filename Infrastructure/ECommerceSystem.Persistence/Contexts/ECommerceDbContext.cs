using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Addresses;
using ECommerceSystem.Domain.Entities.AuditLogs;
using ECommerceSystem.Domain.Entities.Carts;
using ECommerceSystem.Domain.Entities.Inventory;
using ECommerceSystem.Domain.Entities.OrderReturns;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Domain.Entities.Payments;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Domain.Entities.Promotions;
using ECommerceSystem.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.Persistence.Contexts
{
    public class ECommerceDbContext : DbContext
    {

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProductCategory> ProductProductCategories { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<ProductVariation> ProductVariations { get; set; }
        public DbSet<ProductVariationAttribute> ProductVariationAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductWarehouse> ProductWarehouses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderReturn> OrderReturns { get; set; }
        public DbSet<OrderReturnItem> OrderReturnItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<PromotionCategory> PromotionCategories { get; set; }
        public DbSet<PromotionProduct> PromotionProducts { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<TrackingDetail> TrackingDetails { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

    }
}
