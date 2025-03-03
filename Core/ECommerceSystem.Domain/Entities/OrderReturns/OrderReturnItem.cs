using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Orders;

namespace ECommerceSystem.Domain.Entities.OrderReturns
{
    public class OrderReturnItem : CommonId
    {
//        public int OrderReturnItemId { get; set; }
//        public int OrderReturnId { get; set; }
//        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal ItemPrice { get; set; }
        public string ReturnNote { get; set; }

        // Navigation
        public virtual OrderReturn OrderReturn { get; set; }
    }
}
