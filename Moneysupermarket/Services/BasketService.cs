using Moneysupermarket.Model;
using System;
using System.Linq;

namespace Moneysupermarket.Services
{
    public class BasketService : IBasketService
    {
        private IDiscountService discountRules;

        public BasketService(IDiscountService discountRules)
        {
            this.discountRules = discountRules;
        }

        public decimal CalculateTotalValue(Basket basket)
        {
            if (basket == null) 
                throw new Exception("Basket can not be null");
            
            var totalBasket = basket.BasketLines.Sum(l => l.Product.Price * l.Quantity);
            var totalDiscount = discountRules.CalculateDiscount(basket.BasketLines);

            return totalBasket - totalDiscount;
        }
    }
}
