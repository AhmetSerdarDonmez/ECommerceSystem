using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Carts;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Inventory;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Domain.Entities.Promotions;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class Product : CommonTime , ISoftDelete
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ProductDescription { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDeleted { get; set; } = false;


        // Navigation
        public virtual ICollection<CartProduct> CartProducts { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<ProductProductCategory> ProductProductCategories { get; set; }
        public virtual ICollection<ProductVariation> ProductVariations { get; set; }
        public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; }
        public virtual ICollection<StockTransaction> StockTransactions { get; set; }

    }
}
