using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Carts;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Application.Repositories
{
    public class CartProductReadRepository : ReadRepository<CartProduct>, ICartProductReadRepository
    {
        public CartProductReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
