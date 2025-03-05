using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Addresses;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.OrderReturns;
using ECommerceSystem.Domain.Entities.Payments;
using ECommerceSystem.Domain.Entities.Users;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Orders
{
    public class Order :CommonTime,ISoftDelete
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate 
        {
            get => CreatedAt;
            set => CreatedAt = value;
        } 

        public decimal? OrderSubtotalAmount { get; set; }
        public decimal OrderTotalAmount { get; set; }
        public int? UserId { get; set; }
        public string OrderStatus { get; set; }
        public int? ShippingAddressId { get; set; }
        public int? BillingAddressId { get; set; }
        public decimal ShippingCost { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TaxAmount { get; set; } = 0;
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public virtual User User { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public virtual Address BillingAddress { get; set; }
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<TrackingDetail> TrackingDetails { get; set; }
        public virtual ICollection<OrderReturn> OrderReturns { get; set; }


        


    }
}
