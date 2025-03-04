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
    public class TrackingDetailReadRepository : ReadRepository<TrackingDetail>, ITrackingDetailReadRepository
    {
        public TrackingDetailReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
