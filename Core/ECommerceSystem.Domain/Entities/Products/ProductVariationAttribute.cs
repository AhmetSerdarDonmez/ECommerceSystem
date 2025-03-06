using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductVariationAttribute 
    {

        public int VariationId { get; set; }
        public int AttributeValueId { get; set; }

        // Navigation
        public virtual ProductVariation ProductVariation { get; set; } = null!;
        public virtual ProductAttributeValue ProductAttributeValue { get; set; } = null!;

    }
}
