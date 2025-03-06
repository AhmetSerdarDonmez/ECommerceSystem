using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Products;

namespace ECommerceSystem.Domain.Entities.Inventory
{
    public class StockTransaction : CommonTime
    {

        public int StockTransactionId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public int QuantityChange { get; set; }
        public DateTime TransactionDate 
        { 
            get => CreatedAt;
            set => CreatedAt = value;
        } 
        public int? ReferenceOrderId { get; set; }

        // Navigation
        public virtual Product Product { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;
    }
}
