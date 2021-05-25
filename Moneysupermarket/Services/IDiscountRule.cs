using Moneysupermarket.Model;
using System.Collections.Generic;

namespace Moneysupermarket.Services
{
    public interface IDiscountRule
    {
        bool IsMatch(IEnumerable<BasketLine> basketLines);
        decimal GetDiscount(IEnumerable<BasketLine> basketLines);
    }
}
