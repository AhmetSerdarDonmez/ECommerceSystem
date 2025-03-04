﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Application.Repositories
{
    public class CarrierWriteRepository : WriteRepository<Carrier> , ICarrierWriteRepository
    {
        public CarrierWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
