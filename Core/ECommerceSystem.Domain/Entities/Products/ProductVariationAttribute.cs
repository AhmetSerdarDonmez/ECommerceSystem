using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductVariationAttribute : CommonId
    {

//        public int VariationId { get; set; }
//        public int AttributeValueId { get; set; }

        // Navigation
        public virtual ProductVariation Variation { get; set; }
        public virtual ProductAttributeValue AttributeValue { get; set; }

    }
}
