﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Carts;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class CartProductWriteRepository : WriteRepository<CartProduct>, ICartProductWriteRepository
    {
        public CartProductWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
