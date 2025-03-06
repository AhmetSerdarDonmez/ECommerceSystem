using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Products;

namespace ECommerceSystem.Domain.Entities.Carts
{
    public class CartProduct : CommonTime
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public DateTime AddedAt 
        { 
            get => CreatedAt;
            set => CreatedAt = value;
        }
        public DateTime UpdatedAt 
        { 
            get => UpdatedAt;
            set => UpdatedAt = value;
        }
        // Navigation
        public virtual Cart Cart { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
