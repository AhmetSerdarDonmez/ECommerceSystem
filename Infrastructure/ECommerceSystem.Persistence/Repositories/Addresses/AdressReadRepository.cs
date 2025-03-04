using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Addresses;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;
using ECommerceSystem.Application.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class AddressReadRepository : ReadRepository<Address>, IAdressReadRepository
    {
        public AddressReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }

}
