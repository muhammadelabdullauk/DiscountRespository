using Moneysupermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moneysupermarket.Services
{
    public class DiscountService : IDiscountService
    {
        private List<IDiscountRule> discountRules;

        public DiscountService()
        {
            discountRules = new List<IDiscountRule>
            {
                new BuyTwoGetThirdItemHalfPriceDiscountRule(),
                new BuyFourGetOneFreeDiscountRule()
            };
        }

        public decimal CalculateDiscount(IEnumerable<BasketLine> basketLines)
        {
            if(basketLines == null)
                throw new Exception("Basket lines can not be null");

            return discountRules.FindAll(rule => rule.IsMatch(basketLines)).Sum(rule => rule.GetDiscount(basketLines));
        }
    }
}
