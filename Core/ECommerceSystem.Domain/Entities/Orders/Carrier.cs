using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Orders
{
    public class Carrier 
    {

        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }

        // Navigation
        public virtual ICollection<TrackingDetail> TrackingDetails { get; set; }
    }
}
