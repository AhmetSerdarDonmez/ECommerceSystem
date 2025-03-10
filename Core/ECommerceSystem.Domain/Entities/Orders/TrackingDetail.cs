﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Orders
{
    public class TrackingDetail : CommonTime
    {

        public int TrackingDetailId { get; set; }
        public int? OrderId { get; set; }
        public string TrackingStatus { get; set; }
        public DateTime StatusDate 
        {
            get => UpdatedAt;
            set => UpdatedAt = value;
        }
        public string TrackingNumber { get; set; }
        public int? CarrierId { get; set; }
        public string TrackingNote { get; set; }

        // Navigation
        public virtual Order Order { get; set; }
        public virtual Carrier Carrier { get; set; }

    }
}
