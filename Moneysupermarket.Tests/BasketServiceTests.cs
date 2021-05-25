using FluentAssertions;
using Moneysupermarket.Model;
using Moneysupermarket.Services;
using Moneysupermarket.Tests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Moneysupermarket.Tests
{
    [TestFixture]
    public class BasketServiceTests
    {
        private IBasketService basketService;
        private Mock<IDiscountService> mockDiscountService;

        [SetUp]
        public void Setup()
        {
            mockDiscountService = new Mock<IDiscountService>();
            basketService = new BasketService(mockDiscountService.Object);
        }

        [Test]
        public void When_Basket_Is_Null_Exception_Should_be_Thrown()
        {
            //Assign
            Basket basket = null;

            // Act
            Action action = () => basketService.CalculateTotalValue(basket);

            // Assert
            action.Should().Throw<Exception>().WithMessage("Basket can not be null");
        }

        [Test]
        public void When_Basket_Is_Empty_Expect_Total_Zero()
        {
            //Assign
            var basket = new Basket();

            // Act
            var result = basketService.CalculateTotalValue(basket);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void When_Basket_Have_Products_Expect_Total_Basket_Calculated_Correctly()
        {
            //Assign
            var basket = SeedDataHelper.GetBasket();

            // Act
            var result = basketService.CalculateTotalValue(basket);

            // Assert
            Assert.AreEqual(2.95, result);
        }

        [Test]
        public void When_Basket_Have_Two_Butters_And_Two_Breads_Expect_Total_Basket_Calculated_Correctly()
        {
            //Assign
            var basket = new Basket()
            {
                BasketLines = new List<BasketLine>
                {
                    SeedDataHelper.GetButterItems(2),
                    SeedDataHelper.GetBreadItems(2)
                }
            };

            mockDiscountService.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<BasketLine>>())).Returns(0.5M);

            // Act
            var result = basketService.CalculateTotalValue(basket);

            // Assert
            Assert.AreEqual(3.10, result);
        }

        [Test]
        public void When_Basket_Have_Four_Milks_Expect_Total_Basket_Calculated_Correctly()
        {
            //Assign
            var basket = new Basket()
            {
                BasketLines = new List<BasketLine>
                {
                    SeedDataHelper.GetMilkItems(4)
                }
            };

            mockDiscountService.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<BasketLine>>())).Returns(1.15M);

            // Act
            var result = basketService.CalculateTotalValue(basket);

            // Assert
            Assert.AreEqual(3.45, result);
        }

        [Test]
        public void When_Basket_Have_Two_Butters_One_Bread_And_Eight_Milks_Expect_Total_Basket_Calculated_Correctly()
        {
            //Assign
            var basket = new Basket()
            {
                BasketLines = new List<BasketLine>
                {
                    SeedDataHelper.GetButterItems(2),
                    SeedDataHelper.GetBreadItems(1),
                    SeedDataHelper.GetMilkItems(8)
                }
            };

            mockDiscountService.Setup(x => x.CalculateDiscount(It.IsAny<IEnumerable<BasketLine>>())).Returns(2.80M);

            // Act
            var result = basketService.CalculateTotalValue(basket);

            // Assert
            Assert.AreEqual(9, result);
        }
    }
}