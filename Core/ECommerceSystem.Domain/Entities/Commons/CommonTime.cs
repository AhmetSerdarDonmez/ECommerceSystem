using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Domain.Entities.Commons
{
    public class CommonTime
    {

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual void SetCreatedAt()
        {
            CreatedAt = DateTime.UtcNow;
        }
        public virtual void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
