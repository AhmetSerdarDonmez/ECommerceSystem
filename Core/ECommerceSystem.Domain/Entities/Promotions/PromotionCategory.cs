using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Products;

namespace ECommerceSystem.Domain.Entities.Promotions
{
    public class PromotionCategory 
    {

        public int PromotionId { get; set; }
        public int ProductCategoryId { get; set; }

        // Navigation
        public virtual Promotion Promotion { get; set; } = null!;       
        public virtual ProductCategory ProductCategory { get; set; } = null!;

    }
}
