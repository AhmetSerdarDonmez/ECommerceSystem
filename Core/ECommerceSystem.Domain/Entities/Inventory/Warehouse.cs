using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Orders;

namespace ECommerceSystem.Domain.Entities.Inventory
{
    public class Warehouse  
    {

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = string.Empty;
        public string WarehouseAddress { get; set; }


        public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; } = new List<ProductWarehouse>();
        public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();
        public virtual ICollection<OrderProduct> OrderProduct { get; set; } = new List<OrderProduct>();
    }
}
