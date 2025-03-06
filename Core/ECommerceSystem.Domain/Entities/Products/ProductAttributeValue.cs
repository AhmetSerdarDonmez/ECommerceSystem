using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductAttributeValue 
    {

        public int AttributeValueId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; } = string.Empty;

        // Navigation
        public virtual ProductAttribute ProductAttribute { get; set; } = null!;
        public virtual ICollection<ProductVariationAttribute> ProductVariationAttributes { get; set; } = new List<ProductVariationAttribute>();

    }
}
