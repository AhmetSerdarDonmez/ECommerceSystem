﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.AuditLogs;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Application.Repositories
{
    public class AuditLogReadRepository : ReadRepository<AuditLog>, IAuditLogReadRepository
    {
        public AuditLogReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
