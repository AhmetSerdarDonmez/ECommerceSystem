using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Inventory;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Application.Repositories
{
    public class WarehouseWriteRepository : WriteRepository<Warehouse>, IWarehouseWriteRepository
    {
        public WarehouseWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
