using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Carts;

namespace ECommerceSystem.Application.Repositories
{
    public interface ICartReadRepository : IReadRepository<Cart>
    {
    }
}
