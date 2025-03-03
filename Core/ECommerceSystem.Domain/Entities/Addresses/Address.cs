using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;
using ECommerceSystem.Domain.Entities.Users;

namespace ECommerceSystem.Domain.Entities.Addresses
{
    public class Address : CommonId
    {
//        public int AddressId { get; set; }


//        public int? UserId { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }
        public bool IsBillingAddress { get; set; } = false;
        public bool IsShippingAddress { get; set; } = false;

        // Navigation
        public virtual Users.User User { get; set; }
    }
}
