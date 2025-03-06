using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductImage 
    {

        public int ProductImageId { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsThumbnail { get; set; } = false;
        public int SortOrder { get; set; } = 0;

        // Navigation
        public virtual Product Product { get; set; } = null!;

    }
}
