using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Products;

namespace ECommerceSystem.Domain.Entities.Inventory
{
    public class StockTransaction
    {

        public int StockTransactionId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public string TransactionType { get; set; }
        public int QuantityChange { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public int? ReferenceOrderId { get; set; }

        // Navigation
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }

    }
}
