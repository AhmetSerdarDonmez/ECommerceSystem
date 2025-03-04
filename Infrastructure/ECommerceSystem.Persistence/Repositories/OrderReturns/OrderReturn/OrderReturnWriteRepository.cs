using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.OrderReturns;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class OrderReturnWriteRepository : WriteRepository<OrderReturn> , IOrderReturnWriteRepository
    {
        public OrderReturnWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
