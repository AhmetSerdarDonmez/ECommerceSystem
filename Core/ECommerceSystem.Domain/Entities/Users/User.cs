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
    public class User:AuditableEntity,ISoftDelete
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }

        public int? RoleId { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public virtual Role Role { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductReview> ProductReviews { get; set; }
        public virtual ICollection<OrderReturn> OrderReturnsProcessed { get; set; }
        public virtual ICollection<AuditLog> AuditLogs { get; set; }
    }
}
