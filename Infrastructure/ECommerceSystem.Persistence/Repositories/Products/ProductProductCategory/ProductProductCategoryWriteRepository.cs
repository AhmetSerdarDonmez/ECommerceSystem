﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Application.Repositories
{
    public class ProductProductCategoryWriteRepository : WriteRepository<ProductProductCategory> , IProductProductCategoryWriteRepository
    {
        public ProductProductCategoryWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
