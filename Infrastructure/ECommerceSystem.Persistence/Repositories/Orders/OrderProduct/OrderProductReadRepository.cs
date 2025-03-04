using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class OrderProductReadRepository : ReadRepository<OrderProduct> , IOrderProductReadRepository
    {
        public OrderProductReadRepository(ECommerceDbContext context) : base(context)
        {
        }

    }
}
