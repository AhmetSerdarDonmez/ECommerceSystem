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

        private readonly CommonTimeInterceptor _commonTimeInterceptor;

        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options, CommonTimeInterceptor commonTimeInterceptor) : base(options)
        {
            _commonTimeInterceptor = commonTimeInterceptor;
        }

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

        public DbSet<ProductAttributeValue> productAttributeValues { get; set; }

        public DbSet<ProductAttribute> productAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_commonTimeInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // -------------------------------
            // Addresses
            // -------------------------------
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("addresses");
                entity.HasKey(e => e.AddressId).HasName("addresses_pkey");
                entity.Property(e => e.AddressId).HasColumnName("address_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Country).HasColumnName("country").HasMaxLength(100);
                entity.Property(e => e.City).HasColumnName("city").HasMaxLength(100);
                entity.Property(e => e.District).HasColumnName("district").HasMaxLength(100);
                entity.Property(e => e.AddressLine1).HasColumnName("address_line1").HasMaxLength(255);
                entity.Property(e => e.AddressLine2).HasColumnName("address_line2").HasMaxLength(255);
                entity.Property(e => e.PostalCode).HasColumnName("postal_code").HasMaxLength(20);
                entity.Property(e => e.IsBillingAddress).HasColumnName("is_billing_address").HasDefaultValue(false);
                entity.Property(e => e.IsShippingAddress).HasColumnName("is_shipping_address").HasDefaultValue(false);
                // Relationship: Each Address belongs to one User.
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Addresses)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_addresses_user_id");
            });

            // -------------------------------
            // AuditLogs
            // -------------------------------
            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("audit_logs");
                entity.HasKey(e => e.LogId).HasName("audit_logs_pkey");
                entity.Property(e => e.LogId).HasColumnName("log_id");
                entity.Property(e => e.TableName).HasColumnName("table_name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.RecordId).HasColumnName("record_id");
                entity.Property(e => e.ColumnName).HasColumnName("column_name").HasMaxLength(100);
                entity.Property(e => e.OldValue).HasColumnName("old_value");
                entity.Property(e => e.NewValue).HasColumnName("new_value");
                entity.Property(e => e.ChangedByUserId).HasColumnName("changed_by_user_id");
                entity.Property(e => e.ChangedAt).HasColumnName("changed_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TransactionType).HasColumnName("transaction_type").IsRequired().HasMaxLength(10);
                // Relationship: ChangedByUser (optional)
                entity.HasOne(e => e.ChangedByUser)
                      .WithMany()
                      .HasForeignKey(e => e.ChangedByUserId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_audit_logs_user");
            });

            // -------------------------------
            // Carriers
            // -------------------------------
            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("carriers");
                entity.HasKey(e => e.CarrierId).HasName("carriers_pkey");
                entity.Property(e => e.CarrierId).HasColumnName("carrier_id");
                entity.Property(e => e.CarrierName).HasColumnName("carrier_name").IsRequired().HasMaxLength(100);
                entity.Property(e => e.CarrierCode).HasColumnName("carrier_code").HasMaxLength(50);
                entity.HasIndex(e => e.CarrierCode).IsUnique();
                entity.HasIndex(e => e.CarrierName).IsUnique();
            });

            // -------------------------------
            // CartProducts
            // -------------------------------
            modelBuilder.Entity<CartProduct>(entity =>
            {
                entity.ToTable("cart_products");
                entity.HasKey(e => new { e.CartId, e.ProductId }).HasName("cart_products_pkey");
                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.AddedAt).HasColumnName("added_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                // Relationships:
                entity.HasOne(e => e.Cart)
                      .WithMany(c => c.CartProducts)
                      .HasForeignKey(e => e.CartId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_cart_products_cart_id");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.CartProducts)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_cart_products_product_id");
            });

            // -------------------------------
            // Carts
            // -------------------------------
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");
                entity.HasKey(e => e.CartId).HasName("carts_pkey");
                entity.Property(e => e.CartId).HasColumnName("cart_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.CartType).HasColumnName("cart_type").HasMaxLength(50).HasDefaultValue("active");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.UserId);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Carts)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_carts_user_id");
            });

            // -------------------------------
            // OrderProducts
            // -------------------------------
            modelBuilder.Entity<OrderProduct>(entity =>
            {
                entity.ToTable("order_products");
                entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("order_products_pkey");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.ItemPrice).HasColumnName("item_price").HasColumnType("numeric(10,2)");
                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
                // Relationships:
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderProducts)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_order_products_order_id");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.OrderProducts)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_order_products_product_id");
                entity.HasOne(e => e.Warehouse)
                      .WithMany(w => w.OrderProducts)
                      .HasForeignKey(e => e.WarehouseId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_order_products_warehouse_id");
            });


            // -------------------------------
            // OrderReturnItems
            // -------------------------------
            modelBuilder.Entity<OrderReturnItem>(entity =>
            {
                entity.ToTable("order_return_items");
                entity.HasKey(e => e.OrderReturnItemId).HasName("order_return_items_pkey");
                entity.Property(e => e.OrderReturnItemId).HasColumnName("order_return_item_id");
                entity.Property(e => e.OrderReturnId).HasColumnName("order_return_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Amount).HasColumnName("amount");
                entity.Property(e => e.ItemPrice).HasColumnName("item_price").HasColumnType("numeric(10,2)");
                entity.Property(e => e.ReturnNote).HasColumnName("return_note");
                // Relationships:
                entity.HasOne(e => e.OrderReturn)
                      .WithMany(or => or.OrderReturnItems)
                      .HasForeignKey(e => e.OrderReturnId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_order_return_items_order_return_id");
                // Composite FK to OrderProduct.
                entity.HasOne(e => e.OrderProduct)
                      .WithMany() // assuming no reverse navigation property exists
                      .HasForeignKey(e => new { e.OrderId, e.ProductId })
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_order_return_items_order_product");
            });

            // -------------------------------
            // OrderReturns
            // -------------------------------
            modelBuilder.Entity<OrderReturn>(entity =>
            {
                entity.ToTable("order_returns");
                entity.HasKey(e => e.OrderReturnId).HasName("order_returns_pkey");
                entity.Property(e => e.OrderReturnId).HasColumnName("order_return_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.ReturnDate).HasColumnName("return_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.ReturnReason).HasColumnName("return_reason");
                entity.Property(e => e.ReturnStatus).HasColumnName("return_status").HasMaxLength(50).HasDefaultValue("pending");
                entity.Property(e => e.RefundAmount).HasColumnName("refund_amount").HasColumnType("numeric(10,2)").HasDefaultValue(0.00);
                entity.Property(e => e.ApprovedDate).HasColumnName("approved_date");
                entity.Property(e => e.ProcessedBy).HasColumnName("processed_by");
                // Relationships:
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.OrderReturns)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_order_returns_order_id");
                entity.HasOne(e => e.ProcessedByUser)
                      .WithMany() // assuming no reverse navigation
                      .HasForeignKey(e => e.ProcessedBy)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_order_returns_processed_by");
            });

            // -------------------------------
            // Orders
            // -------------------------------
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.OrderId).HasName("orders_pkey");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.OrderNo).HasColumnName("order_no").IsRequired().HasMaxLength(50);
                entity.Property(e => e.OrderDate).HasColumnName("order_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.OrderSubtotalAmount).HasColumnName("order_subtotal_amount").HasColumnType("numeric(10,2)");
                entity.Property(e => e.OrderTotalAmount).HasColumnName("order_total_amount").HasColumnType("numeric(10,2)").IsRequired();
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.OrderStatus).HasColumnName("order_status").HasMaxLength(50);
                entity.Property(e => e.ShippingAddressId).HasColumnName("shipping_address_id");
                entity.Property(e => e.BillingAddressId).HasColumnName("billing_address_id");
                entity.Property(e => e.ShippingCost).HasColumnName("shipping_cost").HasColumnType("numeric(10,2)").HasDefaultValue(0.00);
                entity.Property(e => e.DiscountAmount).HasColumnName("discount_amount").HasColumnType("numeric(10,2)").HasDefaultValue(0.00);
                entity.Property(e => e.TaxAmount).HasColumnName("tax_amount").HasColumnType("numeric(10,2)").HasDefaultValue(0.00);
                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
                entity.HasIndex(e => e.BillingAddressId);
                entity.HasIndex(e => e.ShippingAddressId);
                entity.HasIndex(e => e.UserId);
                // Relationships:
                entity.HasOne(e => e.BillingAddress)
                      .WithMany() // assuming no reverse navigation property exists
                      .HasForeignKey(e => e.BillingAddressId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_orders_billing_address_id");
                entity.HasOne(e => e.ShippingAddress)
                      .WithMany() // similarly
                      .HasForeignKey(e => e.ShippingAddressId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_orders_shipping_address_id");
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_orders_user_id");
            });

            // -------------------------------
            // Payments
            // -------------------------------
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");
                entity.HasKey(e => e.PaymentId).HasName("payments_pkey");
                entity.Property(e => e.PaymentId).HasColumnName("payment_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.PaymentMethod).HasColumnName("payment_method").HasMaxLength(50);
                entity.Property(e => e.Amount).HasColumnName("amount").HasColumnType("numeric(10,2)").IsRequired();
                entity.Property(e => e.PaymentDate).HasColumnName("payment_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status").HasMaxLength(50);
                entity.Property(e => e.TransactionId).HasColumnName("transaction_id").HasMaxLength(255);
                entity.Property(e => e.PaymentGateway).HasColumnName("payment_gateway").HasMaxLength(255);
                entity.Property(e => e.CardLast4Digits).HasColumnName("card_last_4_digits").HasMaxLength(4);
                entity.HasIndex(e => e.OrderId);
                // Relationship:
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.Payments)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_payments_order_id");
            });

            // -------------------------------
            // ProductAttributeValues
            // -------------------------------
            modelBuilder.Entity<ProductAttributeValue>(entity =>
            {
                entity.ToTable("product_attribute_values");
                entity.HasKey(e => e.AttributeValueId).HasName("product_attribute_values_pkey");
                entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");
                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
                entity.Property(e => e.AttributeValue).HasColumnName("attribute_value").IsRequired().HasMaxLength(100);
                entity.HasIndex(e => new { e.AttributeId, e.AttributeValue })
                      .IsUnique().HasDatabaseName("product_attribute_values_unique");
                // Relationship: Each ProductAttributeValue belongs to one ProductAttribute.
                entity.HasOne(e => e.ProductAttribute)
                      .WithMany(a => a.ProductAttributeValues)
                      .HasForeignKey(e => e.AttributeId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_attribute_values_attribute_id");
            });

            // -------------------------------
            // ProductAttributes
            // -------------------------------
            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.ToTable("product_attributes");
                entity.HasKey(e => e.AttributeId).HasName("product_attributes_pkey");
                entity.Property(e => e.AttributeId).HasColumnName("attribute_id");
                entity.Property(e => e.AttributeName).HasColumnName("attribute_name").IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.AttributeName)
                      .IsUnique().HasDatabaseName("product_attributes_attribute_name_key");
            });

            // -------------------------------
            // ProductCategories
            // -------------------------------
            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("product_categories");
                entity.HasKey(e => e.ProductCategoryId).HasName("product_categories_pkey");
                entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
                entity.Property(e => e.CategoryName).HasColumnName("category_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.CategoryDescription).HasColumnName("category_description");
                entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");
                entity.HasIndex(e => e.CategoryName)
                      .IsUnique().HasDatabaseName("product_categories_category_name_key");
            });

            // -------------------------------
            // ProductImages
            // -------------------------------
            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("product_images");
                entity.HasKey(e => e.ProductImageId).HasName("product_images_pkey");
                entity.Property(e => e.ProductImageId).HasColumnName("product_image_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").IsRequired().HasMaxLength(255);
                entity.Property(e => e.IsThumbnail).HasColumnName("is_thumbnail").HasDefaultValue(false);
                entity.Property(e => e.SortOrder).HasColumnName("sort_order").HasDefaultValue(0);
                // Relationship: Each ProductImage belongs to one Product.
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductImages)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_images_product");
            });

            // -------------------------------
            // ProductProductCategories
            // -------------------------------
            modelBuilder.Entity<ProductProductCategory>(entity =>
            {
                entity.ToTable("product_product_categories");
                entity.HasKey(e => new { e.ProductId, e.ProductCategoryId }).HasName("product_product_categories_pkey");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductProductCategories)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_product_categories_product_id");
                entity.HasOne(e => e.ProductCategory)
                      .WithMany(pc => pc.ProductProductCategories)
                      .HasForeignKey(e => e.ProductCategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_product_categories_product_category_id");
            });

            // -------------------------------
            // ProductReviews
            // -------------------------------
            modelBuilder.Entity<ProductReview>(entity =>
            {
                entity.ToTable("product_reviews");
                entity.HasKey(e => e.ReviewId).HasName("product_reviews_pkey");
                entity.Property(e => e.ReviewId).HasColumnName("review_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.Rating).HasColumnName("rating");
                entity.Property(e => e.ReviewText).HasColumnName("review_text").IsRequired(false);
                entity.Property(e => e.ReviewDate).HasColumnName("review_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsApproved).HasColumnName("is_approved").HasDefaultValue(false);
                entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
                // Relationships:
                entity.HasOne(e => e.User)
                      .WithMany(u => u.ProductReviews)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_reviews_user");
                entity.HasOne(e => e.ApprovedByUser)
                      .WithMany()
                      .HasForeignKey(e => e.ApprovedBy)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_product_reviews_approved_by");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductReviews)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_reviews_product");
            });


            // -------------------------------
            // ProductVariationAttributes
            // -------------------------------
            modelBuilder.Entity<ProductVariationAttribute>(entity =>
            {
                entity.ToTable("product_variation_attributes");
                entity.HasKey(e => new { e.VariationId, e.AttributeValueId }).HasName("product_variation_attributes_pkey");
                entity.Property(e => e.VariationId).HasColumnName("variation_id");
                entity.Property(e => e.AttributeValueId).HasColumnName("attribute_value_id");
                entity.HasOne(e => e.ProductVariation)
                      .WithMany(p => p.ProductVariationAttributes)
                      .HasForeignKey(e => e.VariationId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_variation_attributes_variation");
                entity.HasOne(e => e.ProductAttributeValue)
                      .WithMany(av => av.ProductVariationAttributes)
                      .HasForeignKey(e => e.AttributeValueId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_variation_attributes_attribute_value");
            });

            // -------------------------------
            // ProductVariations
            // -------------------------------
            modelBuilder.Entity<ProductVariation>(entity =>
            {
                entity.ToTable("product_variations");
                entity.HasKey(e => e.VariationId).HasName("product_variations_pkey");
                entity.Property(e => e.VariationId).HasColumnName("variation_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Sku).HasColumnName("sku").IsRequired().HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("numeric(10,2)").IsRequired();
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity").IsRequired();
                entity.HasIndex(e => e.Sku).IsUnique();
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductVariations)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_variations_product");
            });

            // -------------------------------
            // ProductWarehouses
            // -------------------------------
            modelBuilder.Entity<ProductWarehouse>(entity =>
            {
                entity.ToTable("product_warehouses");
                entity.HasKey(e => new { e.ProductId, e.WarehouseId }).HasName("product_warehouses_pkey");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
                entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.ProductWarehouses)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_warehouses_product_id");
                entity.HasOne(e => e.Warehouse)
                      .WithMany(w => w.ProductWarehouses)
                      .HasForeignKey(e => e.WarehouseId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_product_warehouses_warehouse_id");
            });

            // -------------------------------
            // Products
            // -------------------------------
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");
                entity.HasKey(e => e.ProductId).HasName("products_pkey");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.ProductName).HasColumnName("product_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Price).HasColumnName("price").HasColumnType("numeric(10,2)").IsRequired();
                entity.Property(e => e.ProductDescription).HasColumnName("product_description");
                entity.Property(e => e.ImageUrl).HasColumnName("image_url").HasMaxLength(255);
                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // -------------------------------
            // PromotionCategories
            // -------------------------------
            modelBuilder.Entity<PromotionCategory>(entity =>
            {
                entity.ToTable("promotion_categories");
                entity.HasKey(e => new { e.PromotionId, e.ProductCategoryId }).HasName("promotion_categories_pkey");
                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
                entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
                entity.HasOne(e => e.ProductCategory)
                      .WithMany(pc => pc.PromotionCategories)
                      .HasForeignKey(e => e.ProductCategoryId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_promotion_categories_category");
                entity.HasOne(e => e.Promotion)
                      .WithMany(p => p.PromotionCategories)
                      .HasForeignKey(e => e.PromotionId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_promotion_categories_promotion");
            });

            // -------------------------------
            // PromotionProducts
            // -------------------------------
            modelBuilder.Entity<PromotionProduct>(entity =>
            {
                entity.ToTable("promotion_products");
                entity.HasKey(e => new { e.PromotionId, e.ProductId }).HasName("promotion_products_pkey");
                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.PromotionProducts)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_promotion_products_product");
                entity.HasOne(e => e.Promotion)
                      .WithMany(p => p.PromotionProducts)
                      .HasForeignKey(e => e.PromotionId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_promotion_products_promotion");
            });

            // -------------------------------
            // Promotions
            // -------------------------------
            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.ToTable("promotions");
                entity.HasKey(e => e.PromotionId).HasName("promotions_pkey");
                entity.Property(e => e.PromotionId).HasColumnName("promotion_id");
                entity.Property(e => e.PromotionName).HasColumnName("promotion_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PromotionDescription).HasColumnName("promotion_description");
                entity.Property(e => e.DiscountType).HasColumnName("discount_type").IsRequired().HasMaxLength(50);
                entity.Property(e => e.DiscountValue).HasColumnName("discount_value").HasColumnType("numeric(10,2)").IsRequired();
                entity.Property(e => e.StartDate).HasColumnName("start_date").IsRequired();
                entity.Property(e => e.EndDate).HasColumnName("end_date").IsRequired();
                entity.Property(e => e.CouponCode).HasColumnName("coupon_code").HasMaxLength(50);
                entity.Property(e => e.MinimumOrderAmount).HasColumnName("minimum_order_amount").HasColumnType("numeric(10,2)").HasDefaultValue(0.00);
                entity.Property(e => e.AppliesTo).HasColumnName("applies_to").IsRequired().HasMaxLength(50);
            });

            // -------------------------------
            // Roles
            // -------------------------------
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");
                entity.HasKey(e => e.RoleId).HasName("roles_pkey");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.RoleName).HasColumnName("role_name").IsRequired().HasMaxLength(50);
                entity.Property(e => e.RoleDescription).HasColumnName("role_description");
                entity.HasIndex(e => e.RoleName).IsUnique().HasDatabaseName("roles_role_name_key");
            });

            // -------------------------------
            // StockTransactions
            // -------------------------------
            modelBuilder.Entity<StockTransaction>(entity =>
            {
                entity.ToTable("stock_transactions");
                entity.HasKey(e => e.StockTransactionId).HasName("stock_transactions_pkey");
                entity.Property(e => e.StockTransactionId).HasColumnName("stock_transaction_id");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
                entity.Property(e => e.TransactionType).HasColumnName("transaction_type").IsRequired().HasMaxLength(50);
                entity.Property(e => e.QuantityChange).HasColumnName("quantity_change");
                entity.Property(e => e.TransactionDate).HasColumnName("transaction_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.ReferenceOrderId).HasColumnName("reference_order_id");
                entity.HasIndex(e => e.ProductId);
                entity.HasIndex(e => e.WarehouseId);
                entity.HasOne(e => e.Product)
                      .WithMany(p => p.StockTransactions)
                      .HasForeignKey(e => e.ProductId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_stock_transactions_product_id");
                entity.HasOne(e => e.Warehouse)
                      .WithMany(w => w.StockTransactions)
                      .HasForeignKey(e => e.WarehouseId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("fk_stock_transactions_warehouse_id");
            });

            // -------------------------------
            // TrackingDetails
            // -------------------------------
            modelBuilder.Entity<TrackingDetail>(entity =>
            {
                entity.ToTable("tracking_details");
                entity.HasKey(e => e.TrackingDetailId).HasName("tracking_details_pkey");
                entity.Property(e => e.TrackingDetailId).HasColumnName("tracking_detail_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.TrackingStatus).HasColumnName("tracking_status").HasMaxLength(50);
                entity.Property(e => e.StatusDate).HasColumnName("status_date").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.TrackingNumber).HasColumnName("tracking_number").HasMaxLength(255);
                entity.Property(e => e.CarrierId).HasColumnName("carrier_id");
                entity.Property(e => e.TrackingNote).HasColumnName("tracking_note");
                entity.HasIndex(e => e.OrderId);
                entity.HasOne(e => e.Carrier)
                      .WithMany(c => c.TrackingDetails)
                      .HasForeignKey(e => e.CarrierId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_tracking_details_carrier_id");
                entity.HasOne(e => e.Order)
                      .WithMany(o => o.TrackingDetails)
                      .HasForeignKey(e => e.OrderId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_tracking_details_order_id");
            });

            // -------------------------------
            // Users
            // -------------------------------
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity.HasKey(e => e.UserId).HasName("users_pkey");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.UserName).HasColumnName("user_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(255);
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
                entity.Property(e => e.RoleId).HasColumnName("role_id").HasDefaultValue(1);
                entity.Property(e => e.IsDeleted).HasColumnName("is_deleted").HasDefaultValue(false);
                entity.Property(e => e.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.Email).IsUnique().HasDatabaseName("users_email_key");
                entity.HasOne(e => e.Role)
                      .WithMany(r => r.Users)
                      .HasForeignKey(e => e.RoleId)
                      .OnDelete(DeleteBehavior.SetNull)
                      .HasConstraintName("fk_users_role_id");
            });

            // -------------------------------
            // Warehouses
            // -------------------------------
            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("warehouses");
                entity.HasKey(e => e.WarehouseId).HasName("warehouses_pkey");
                entity.Property(e => e.WarehouseId).HasColumnName("warehouse_id");
                entity.Property(e => e.WarehouseName).HasColumnName("warehouse_name").IsRequired().HasMaxLength(255);
                entity.Property(e => e.WarehouseAddress).HasColumnName("warehouse_address");
            });



        }
    }
}

