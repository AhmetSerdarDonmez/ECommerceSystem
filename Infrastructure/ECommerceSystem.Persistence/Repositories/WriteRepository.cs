using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceSystem.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class
    {
        readonly private ECommerceDbContext _context;

        public WriteRepository(ECommerceDbContext contect)
        {
            _context = contect;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T model)
        {

            EntityEntry<T> entityEntry = await Table.AddAsync(model);

            return entityEntry.State == EntityState.Added;


        }

        public async Task<bool> AddRangeAsync(List<T> model)
        {
            await Table.AddRangeAsync(model);
            return true;

        }

        public bool Remove(T model)   
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> models)
        {
            Table.RemoveRange(models);
            return true;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FindAsync(int.Parse(id));
            return Remove(model);
            
            
        }

        public async Task<int> SaveAsync()
        {
           return await _context.SaveChangesAsync();
        }

        public bool Update(T model)
        {
            EntityEntry<T> entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }
    }
}
