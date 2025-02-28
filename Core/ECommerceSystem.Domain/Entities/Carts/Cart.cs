using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.Carts
{
    public class Cart
    {
        public int CartId { get; set; }
        public int? UserId { get; set; }
        public string CartType { get; set; } 

        
        public virtual User User { get; set; }
        public virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}
