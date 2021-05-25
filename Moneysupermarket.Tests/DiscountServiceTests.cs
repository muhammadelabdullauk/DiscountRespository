using FluentAssertions;
using Moneysupermarket.Model;
using Moneysupermarket.Services;
using Moneysupermarket.Tests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Moneysupermarket.Tests
{
    [TestFixture]
    public class DiscountServiceTests
    {
        private IDiscountService discountService;

        [SetUp]
        public void Setup()
        {
            discountService = new DiscountService();
        }

        [Test]
        public void When_BasketLines_Is_Null_Exception_Should_be_Thrown()
        {
            //Assign
            List<BasketLine> basketLines = null;

            // Act
            Action action = () => discountService.CalculateDiscount(basketLines);

            // Assert
            action.Should().Throw<Exception>().WithMessage("Basket lines can not be null");
        }

        [TestCaseSource(nameof(NoDiscountTestCases))]
        public void When_No_Discount_Is_Apllied_Expect_Total_Discount_Be_Zero(List<BasketLine> basketLines)
        {
            // Act
            var result = discountService.CalculateDiscount(basketLines);

            // Assert
            Assert.AreEqual(0, result);
        }

        [TestCaseSource(nameof(BuyTwoGetThirdItemHalfPriceDiscountTestCases))]
        public void When_Discount_BuyTwoGetThirdItemHalfPrice_Is_Applied_Expect_Total_Discout_Be_Half_The_Third_Item_Price(List<BasketLine> basketLines, decimal expectedDiscount)
        {
            // Act
            var result = discountService.CalculateDiscount(basketLines);

            // Assert
            Assert.AreEqual(expectedDiscount, result);
        }

        [TestCaseSource(nameof(BuyFourGetOneFreeDiscountTestCases))]
        public void When_Discount_BuyFourGetOneFree_Is_Applied_Expect_Total_Discout_Be_An_Item_Price(List<BasketLine> basketLines, decimal expectedDiscount)
        {
            // Act
            var result = discountService.CalculateDiscount(basketLines);

            // Assert
            Assert.AreEqual(expectedDiscount, result);
        }


        [TestCaseSource(nameof(MultipleDiscountsTestCases))]
        public void When_Multiple_Discounts_Is_Applied_Expect_Total_Discout_Be_Calculated_Correctly(List<BasketLine> basketLines, decimal expectedDiscount)
        {
            // Act
            var result = discountService.CalculateDiscount(basketLines);

            // Assert
            Assert.AreEqual(expectedDiscount, result);
        }

        private static IEnumerable<TestCaseData> NoDiscountTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(1), SeedDataHelper.GetBreadItems(1), SeedDataHelper.GetMilkItems(1) }),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(1), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(2) }),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(1), SeedDataHelper.GetBreadItems(3), SeedDataHelper.GetMilkItems(2) }),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(1), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(3) })
            };
        }

        private static IEnumerable<TestCaseData> BuyTwoGetThirdItemHalfPriceDiscountTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(2), SeedDataHelper.GetBreadItems(2) }, 0.5M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(3), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(2) },  0.5M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(4), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(2) }, 1M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(5), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(3) }, 1M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(6), SeedDataHelper.GetBreadItems(2) }, 1M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(6), SeedDataHelper.GetBreadItems(3) }, 1.5M),
            };
        }

        private static IEnumerable<TestCaseData> BuyFourGetOneFreeDiscountTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(4) }, 1.15M),
                new TestCaseData(
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(5), SeedDataHelper.GetBreadItems(2) },  1.15M),
                new TestCaseData(                        
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(6), SeedDataHelper.GetButterItems(2) },  1.15M),
                new TestCaseData(                        
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(7), SeedDataHelper.GetBreadItems(3) }, 1.15M),
                new TestCaseData(                        
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(8) }, 2.3M),
                new TestCaseData(                        
                    new List<BasketLine> {SeedDataHelper.GetMilkItems(9) }, 2.3M),
            };
        }

        private static IEnumerable<TestCaseData> MultipleDiscountsTestCases()
        {
            return new[]
            {
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(2), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(4) }, 1.65M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(3), SeedDataHelper.GetBreadItems(1), SeedDataHelper.GetMilkItems(3) }, 0.5M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(4), SeedDataHelper.GetBreadItems(2), SeedDataHelper.GetMilkItems(5) }, 2.15M),
                new TestCaseData(
                    new List<BasketLine> { SeedDataHelper.GetButterItems(4), SeedDataHelper.GetBreadItems(3), SeedDataHelper.GetMilkItems(9) }, 3.3M)
            };
        }
    }
}
