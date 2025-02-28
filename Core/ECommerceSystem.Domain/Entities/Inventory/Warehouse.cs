using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Orders;

namespace ECommerceSystem.Domain.Entities.Inventory
{
    public class Warehouse
    {

        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }


        public virtual ICollection<ProductWarehouse> ProductWarehouses { get; set; }
        public virtual ICollection<StockTransaction> StockTransactions { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }

    }
}
