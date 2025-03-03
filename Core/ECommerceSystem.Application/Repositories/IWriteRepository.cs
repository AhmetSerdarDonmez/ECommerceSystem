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

        Task<bool> AddAsync(List<T> model);

        Task<bool> UpdateAsync(T model);

        Task<bool> Remove(T model);

        Task<bool> Remove(string id);

        Task<bool> SaveAsync();
    }
}
