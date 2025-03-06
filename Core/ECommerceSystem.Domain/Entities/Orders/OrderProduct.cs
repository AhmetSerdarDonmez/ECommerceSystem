using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Domain.Entities.Inventory;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Orders
{
    public class OrderProduct 
    {
        public int ProductId { get; set; }

        public int OrderId { get; set; }

        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }

        public int WarehouseId { get; set; }


        public Order Order { get; set; } = null!; 
        public Product Product { get; set; } = null!; 
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public virtual Warehouse Warehouses { get; set; }

    }
}
