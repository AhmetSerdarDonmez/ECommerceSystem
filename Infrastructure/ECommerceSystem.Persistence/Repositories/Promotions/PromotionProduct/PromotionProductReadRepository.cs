﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Promotions;
using ECommerceSystem.Persistence.Contexts;

namespace ECommerceSystem.Persistence.Repositories
{
    public class PromotionProductReadRepository : ReadRepository<PromotionProduct> , IPromotionProductReadRepository
    {
        public PromotionProductReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
