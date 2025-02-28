using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Domain.Entities.Commons
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
