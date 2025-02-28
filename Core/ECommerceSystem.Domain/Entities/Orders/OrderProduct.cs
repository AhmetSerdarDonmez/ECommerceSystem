using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Domain.Entities.Inventory;

namespace ECommerceSystem.Domain.Entities.Orders
{
    public class OrderProduct
    {
        
        public int amount { get; set; }
        public decimal item_price { get; set; }



        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public virtual ICollection<Warehouse> Warehouses { get; set; }

    }
}
