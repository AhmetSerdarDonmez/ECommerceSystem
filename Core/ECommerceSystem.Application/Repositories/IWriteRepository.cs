using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : CommonId
    {
        Task<bool> AddAsync(T model);

        Task<bool> AddRangeAsync(List<T> model);

        bool Update(T model);

        bool Remove(T model);
        bool RemoveRange(List<T> models);
        Task<bool> RemoveAsync(string id);

        Task<int> SaveAsync();
    }
}
