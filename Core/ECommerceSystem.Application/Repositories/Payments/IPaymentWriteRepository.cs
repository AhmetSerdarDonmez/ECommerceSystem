using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Payments;

namespace ECommerceSystem.Application.Repositories
{
    public interface IPaymentWriteRepository:IWriteRepository<Payment>
    {
    }
}
