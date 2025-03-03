using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : CommonId
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhereAsync(Expression<Func<T, bool>> expression);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> expression);

        Task<T> GetByIdAsync(string id);


    }
}
