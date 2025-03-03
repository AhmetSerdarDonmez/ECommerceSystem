using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Orders;

namespace ECommerceSystem.Domain.Entities.Payments
{
    public class Payment : CommonId
    {

 //       public int PaymentId { get; set; }
 //       public int? OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public string PaymentGateway { get; set; }
        public string CardLast4Digits { get; set; }


        // Navigation
        public virtual Order Order { get; set; }
    }
}
