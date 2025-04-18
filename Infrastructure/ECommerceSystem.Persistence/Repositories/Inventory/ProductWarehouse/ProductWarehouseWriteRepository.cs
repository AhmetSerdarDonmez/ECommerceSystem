﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Inventory;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class ProductWarehouseWriteRepository : WriteRepository<ProductWarehouse>, IProductWarehouseWriteRepository
    {
        public ProductWarehouseWriteRepository(ECommerceDbContext context) : base(context)
        {
        }

    }
}
