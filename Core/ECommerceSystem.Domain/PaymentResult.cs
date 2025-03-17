using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceSystem.Domain
{
    public class PaymentResult
    {
        public bool IsSuccessful { get; set; }
        public string PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}
