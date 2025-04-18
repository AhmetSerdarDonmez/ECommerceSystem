﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductProductCategory 
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }

        // Navigation
        public virtual Product Product { get; set; } = null!;
        public virtual ProductCategory ProductCategory { get; set; } = null!;
    }
}
