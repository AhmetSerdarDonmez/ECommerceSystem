using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class ProductReadRepository:ReadRepository<Product> , IProductReadRepository
    {
        public ProductReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
