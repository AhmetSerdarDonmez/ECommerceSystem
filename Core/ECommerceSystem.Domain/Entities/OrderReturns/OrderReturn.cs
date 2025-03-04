using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Orders;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.OrderReturns
{
    public class OrderReturn 
    {
        public int OrderReturnId { get; set; }
        public int? OrderId { get; set; }
        public DateTime ReturnDate { get; set; } = DateTime.UtcNow;
        public string ReturnReason { get; set; }
        public string ReturnStatus { get; set; } = "pending";
        public decimal RefundAmount { get; set; } = 0;
        public DateTime? ApprovedDate { get; set; }
        public int? ProcessedBy { get; set; }

        
        public virtual Order Order { get; set; }
        public virtual User ProcessedByUser { get; set; }
        public virtual ICollection<OrderReturnItem> OrderReturnItems { get; set; }
    }
}
