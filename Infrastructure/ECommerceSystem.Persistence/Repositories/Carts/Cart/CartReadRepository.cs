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
    public class CartReadRepository : ReadRepository<Cart>, ICartReadRepository
    {
        public CartReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }

}
