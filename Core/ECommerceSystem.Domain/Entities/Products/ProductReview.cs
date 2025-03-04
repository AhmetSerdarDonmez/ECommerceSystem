using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.Products
{
    public class ProductReview 
    {
        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public int? ApprovedBy { get; set; }

        // Navigation
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
        public virtual User ApprovedByUser { get; set; }
    }
}
