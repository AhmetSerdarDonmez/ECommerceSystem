using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Addresses;
using ECommerceSystem.Domain.Entities.Carts;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Domain.Entities.Products;
using ECommerceSystem.Domain.Entities.OrderReturns;
using ECommerceSystem.Domain.Entities.AuditLogs;


namespace ECommerceSystem.Domain.Entities.Users
{
    public class User :CommonTime, ISoftDelete
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; } = false;



        // Navigation
        public virtual Role? Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public virtual ICollection<OrderReturn> OrderReturnsProcessed { get; set; } = new List<OrderReturn>();
        public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}
