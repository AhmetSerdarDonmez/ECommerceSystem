using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.Users
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = "active";
        public string RoleDescription { get; set; }

        // Navigation
        public virtual ICollection<User> Users { get; set; }
    }
}
