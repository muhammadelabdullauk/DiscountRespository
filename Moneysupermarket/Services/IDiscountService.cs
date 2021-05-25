using Moneysupermarket.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneysupermarket.Services
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(IEnumerable<BasketLine> basketLines); 
    }
}
