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
    public class StockTransactionReadRepository : ReadRepository<StockTransaction>, IStockTransactionReadRepository
    {
        public StockTransactionReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
