using Moneysupermarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moneysupermarket.Services
{
    class BuyTwoGetThirdItemHalfPriceDiscountRule : IDiscountRule
    {
        public bool IsMatch(IEnumerable<BasketLine> basketLines)
        {
            var dicountProducts = basketLines.Select(bl => bl.Product).Where(p => p.Discount?.DiscountType == DiscountType.BuyTwoGetThirdItemHalfPrice);
            if (dicountProducts.Any())
            {
                foreach (var dicountProduct in dicountProducts)
                {
                    if (basketLines.Any(bl => bl.Product.Name == dicountProduct.Discount.AffectedProduct))
                        return true;
                }
            }
            return false;
        }
        public decimal GetDiscount(IEnumerable<BasketLine> basketLines)
        {
            var dicountProducts = basketLines
                .Where(p => p.Product.Discount?.DiscountType == DiscountType.BuyTwoGetThirdItemHalfPrice)
                .GroupBy(p => p.Product.Name)
                .Select(bl => new
                {
                    ProductName = bl.First().Product.Name,
                    TotalItems = bl.Sum(bl => bl.Quantity),
                    AffectedProduct = bl.First().Product.Discount?.AffectedProduct
                });

            decimal totalDiscount = 0;

            foreach (var dicountProduct in dicountProducts)
            {
                var productGroups = Math.Truncate((decimal)dicountProduct.TotalItems / 2);
                if (productGroups > 0)
                {
                    var totalAffectedProducts = basketLines.Where(bl => bl.Product.Name == dicountProduct.AffectedProduct).Sum(bl => bl.Quantity);
                    decimal? priceAffectedProduct = basketLines.Select(bl => bl.Product).First(p => p.Name == dicountProduct.AffectedProduct)?.Price;
                    if (totalAffectedProducts >= productGroups)
                        totalDiscount += (productGroups * priceAffectedProduct.GetValueOrDefault() * 0.5M);
                    else
                        totalDiscount += (totalAffectedProducts * priceAffectedProduct.GetValueOrDefault() * 0.5M);
                }
            }

            return totalDiscount;
        }
    }
}
