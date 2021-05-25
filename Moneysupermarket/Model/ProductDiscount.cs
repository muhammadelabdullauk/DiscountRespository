using System;
using System.Collections.Generic;
using System.Text;

namespace Moneysupermarket.Model
{
    public class ProductDiscount
    {
        public DiscountType DiscountType { get; set; }
        public string AffectedProduct { get; set; }
    }
}
