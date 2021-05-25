using Moneysupermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moneysupermarket.Services
{
    public class BuyFourGetOneFreeDiscountRule : IDiscountRule
    {
        public bool IsMatch(IEnumerable<BasketLine> basketLines)
        {
            var dicountProducts = basketLines.Select(bl => bl.Product).Where(p => p.Discount?.DiscountType == DiscountType.BuyFourGetOneFree);
            return dicountProducts.Count() > 0;
        }

        public decimal GetDiscount(IEnumerable<BasketLine> basketLines)
        {
            var dicountProducts = basketLines
                .Where(p => p.Product.Discount?.DiscountType == DiscountType.BuyFourGetOneFree)
                .GroupBy(p => p.Product.Name)
                .Select(bl => new
                {
                    ProductName = bl.First().Product.Name,
                    TotalItems = bl.Sum(bl => bl.Quantity)
                });

            decimal totalDiscount = 0;

            foreach (var dicountProduct in dicountProducts)
            {
                var productGroups = Math.Truncate((decimal)dicountProduct.TotalItems / 4);
                if (productGroups > 0)
                {
                    decimal? productPrice = basketLines.Select(bl => bl.Product).First(p => p.Name == dicountProduct.ProductName).Price;
                    totalDiscount += productGroups * productPrice.GetValueOrDefault();
                }
            }

            return totalDiscount;
        }
    }
}
