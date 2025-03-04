using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Application.Repositories;
using ECommerceSystem.Domain.Entities.Payments;
using ECommerceSystem.Persistence.Contexts;
using ECommerceSystem.Persistence.Repositories;

namespace ECommerceSystem.Persistence.Repositories
{
    public class PaymentWriteRepository:WriteRepository<Payment> , IPaymentWriteRepository
    {
        public PaymentWriteRepository(ECommerceDbContext context) : base(context)
        {
        }
    }
}
