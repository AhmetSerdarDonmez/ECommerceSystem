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
    public class ProductImageWriteRepository : WriteRepository<ProductImage> , IProductImageWriteRepository
    {
        public ProductImageWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
