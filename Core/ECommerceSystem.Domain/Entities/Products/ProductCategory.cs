using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Promotions;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductCategory 
    {

        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int? ParentCategoryId { get; set; }

        // Navigation
        public virtual ProductCategory ParentCategory { get; set; }
        public virtual ICollection<ProductProductCategory> ProductProductCategories { get; set; }
        public virtual ICollection<PromotionCategory> PromotionCategories { get; set; } 
    }
}
