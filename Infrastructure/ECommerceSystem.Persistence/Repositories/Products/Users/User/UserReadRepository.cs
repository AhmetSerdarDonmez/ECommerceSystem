using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Users;
using ECommerceSystem.Persistence.Contexts;

namespace ECommerceSystem.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User> , IUserReadRepository
    {
        public UserReadRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
