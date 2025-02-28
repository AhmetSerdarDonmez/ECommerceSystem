using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductAttribute
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }

        // Navigation
        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }

    }
}
