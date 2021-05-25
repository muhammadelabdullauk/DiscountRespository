using Moneysupermarket.Model;
using System.Collections.Generic;

namespace Moneysupermarket.Tests.Helpers
{
    public static class SeedDataHelper
    {
        public static Basket GetBasket()
        {
            return new Basket
            {
                BasketLines = new List<BasketLine>
                {
                    new BasketLine { Id = 1, Product = GetProductButter(), Quantity = 1 },
                    new BasketLine { Id = 1, Product = GetProductMilk(), Quantity = 1 },
                    new BasketLine { Id = 1, Product = GetProductBread(), Quantity = 1 },
                }
            };
        }

        public static BasketLine GetButterItems(int quantity) => new BasketLine { Product = GetProductButter(), Quantity = quantity };
        public static BasketLine GetMilkItems(int quantity) => new BasketLine { Product = GetProductMilk(), Quantity = quantity, };
        public static BasketLine GetBreadItems(int quantity) => new BasketLine { Product = GetProductBread(), Quantity = quantity };

        private static Product GetProductButter() => new Product
        {
            Id = 1,
            Name = "Butter",
            Description = "British Unsalted Butter 250G",
            Price = 0.8M,
            Discount = new ProductDiscount 
            {
                DiscountType = DiscountType.BuyTwoGetThirdItemHalfPrice,
                AffectedProduct = "Bread"
            }            
        };
        private static Product GetProductMilk() => new Product
        {
            Id = 2,
            Name = "Milk",
            Description = "Whole milk",
            Price = 1.15M,
            Discount = new ProductDiscount
            {
                DiscountType = DiscountType.BuyFourGetOneFree,
                AffectedProduct = "Milk"
            }
        };
        private static Product GetProductBread() => new Product { Id = 3, Name = "Bread", Description = "Wholemeal Medium Bread 800G", Price = 1M };
    }
}
