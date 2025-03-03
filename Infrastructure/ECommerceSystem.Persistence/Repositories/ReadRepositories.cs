using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceSystem.Persistence.Repositories
{
    class ReadRepositories<T> : IReadRepository<T> where T : CommonId
    {
        private readonly ECommerceDbContext _context;


        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll() => Table;

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetWhereAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
