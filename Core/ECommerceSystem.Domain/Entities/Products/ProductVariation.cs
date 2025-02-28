using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductVariation
    {

        public int VariationId { get; set; }
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        // Navigation
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductVariationAttribute> ProductVariationAttributes { get; set; }


    }
}
