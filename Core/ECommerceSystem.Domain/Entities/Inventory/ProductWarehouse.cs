﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Products;

namespace ECommerceSystem.Domain.Entities.Inventory
{
    public class ProductWarehouse 
    {

        public int ProductId { get; set; }
        public int WarehouseId { get; set; }
        public int StockQuantity { get; set; }

        // Navigation
        public virtual Product Product { get; set; } = null!;
        public virtual Warehouse Warehouse { get; set; } = null!;

        public ICollection<StockTransaction> StockTransactions { get; set; }= new List<StockTransaction>();
    }
}
