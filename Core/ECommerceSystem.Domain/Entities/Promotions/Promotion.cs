﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceSystem.Domain.Entities.Commons;

namespace ECommerceSystem.Domain.Entities.Promotions
{
    public class Promotion : CommonTime
    {

        public int PromotionId { get; set; }
        public string PromotionName { get; set; } = string.Empty;
        public string PromotionDescription { get; set; }
        public string DiscountType { get; set; } = string.Empty;
        public decimal DiscountValue { get; set; }
        public DateTime StartDate 
        { 
            get => CreatedAt;
            set => CreatedAt = value;
        }
        public DateTime EndDate { get; set; }
        public string CouponCode { get; set; }
        public decimal MinimumOrderAmount { get; set; } = 0;
        public string AppliesTo { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<PromotionCategory> PromotionCategories { get; set; } = new List<PromotionCategory>();
        public virtual ICollection<PromotionProduct> PromotionProducts { get; set; } = new List<PromotionProduct>();

    }
}
