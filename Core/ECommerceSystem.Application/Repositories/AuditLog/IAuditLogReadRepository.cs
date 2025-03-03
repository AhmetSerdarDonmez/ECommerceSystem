using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.AuditLogs;

namespace ECommerceSystem.Application.Repositories
{
    public interface IAuditLogReadRepository : IReadRepository<AuditLog>
    {
    }
}
