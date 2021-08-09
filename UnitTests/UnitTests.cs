using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PromotionEngine;

namespace UnitTests
{
    using NSubstitute.Core;

    [TestClass]
    public class UnitTests
    {
        private class Context
        {
            public IItem ItemA { get; set; }
            public IItem ItemB { get; set; }
            public IItem ItemC { get; set; }
            public IItem ItemD { get; set; }

            public ICart ScenarioA { get; set; }
            public ICart ScenarioB { get; set; }
            public ICart ScenarioC { get; set; }

            public IPromotion Promotion3A { get; set; }
            public IPromotion Promotion2B { get; set; }
            public IPromotion PromotionCD { get; set; }
        }

        private Context CreatePromotions()
        {
            var result = new Context
            {
                ItemA = Substitute.For<IItem>(),
                ItemB = Substitute.For<IItem>(),
                ItemC = Substitute.For<IItem>(),
                ItemD = Substitute.For<IItem>(),

                ScenarioA = Substitute.For<ICart>(),
                ScenarioB = Substitute.For<ICart>(),
                ScenarioC = Substitute.For<ICart>(),

                Promotion3A = Substitute.For<IPromotion>(),
                Promotion2B = Substitute.For<IPromotion>(),
                PromotionCD = Substitute.For<IPromotion>(),
            };

            result.ItemA.SKU.Returns("A");
            result.ItemA.UnitPrice.Returns(50);

            result.ItemB.SKU.Returns("B");
            result.ItemB.UnitPrice.Returns(30);

            result.ItemC.SKU.Returns("C");
            result.ItemC.UnitPrice.Returns(20);

            result.ItemD.SKU.Returns("D");
            result.ItemD.UnitPrice.Returns(15);

            #region Scenario A

            var ci1 = Substitute.For<ICartItem>();
            ci1.IsPromoted.Returns(false);
            ci1.Item.Returns(result.ItemA);
            ci1.Amount.Returns(1);

            var ci2 = Substitute.For<ICartItem>();
            ci1.IsPromoted.Returns(false);
            ci1.Item.Returns(result.ItemB);
            ci1.Amount.Returns(1);

            var ci3 = Substitute.For<ICartItem>();
            ci1.IsPromoted.Returns(false);
            ci1.Item.Returns(result.ItemC);
            ci1.Amount.Returns(1);

            #endregion

            result.ScenarioA.Items.Returns(new List<ICartItem>() {ci1, ci2, ci3});
            result.ScenarioA.Sum.Returns(100);

            #region Scenario B

            var cib1 = Substitute.For<ICartItem>();
            cib1.IsPromoted.Returns(false);
            cib1.Item.Returns(result.ItemA);
            cib1.Amount.Returns(5);

            var cib2 = Substitute.For<ICartItem>();
            cib2.IsPromoted.Returns(false);
            cib2.Item.Returns(result.ItemB);
            cib2.Amount.Returns(5);

            var cib3 = Substitute.For<ICartItem>();
            cib3.IsPromoted.Returns(false);
            cib3.Item.Returns(result.ItemC);
            cib3.Amount.Returns(1);

            #endregion

            result.ScenarioB.Items.Returns(new List<ICartItem>() {cib1, cib2, cib3});
            result.ScenarioB.Sum.Returns(420); // original value, without promotion

            #region Scenario C

            var cic1 = Substitute.For<ICartItem>();
            cic1.IsPromoted.Returns(false);
            cic1.Item.Returns(result.ItemA);
            cic1.Amount.Returns(3);

            var cic2 = Substitute.For<ICartItem>();
            cic2.IsPromoted.Returns(false);
            cic2.Item.Returns(result.ItemB);
            cic2.Amount.Returns(5);

            var cic3 = Substitute.For<ICartItem>();
            cic3.IsPromoted.Returns(false);
            cic3.Item.Returns(result.ItemC);
            cic3.Amount.Returns(1);

            var cic4 = Substitute.For<ICartItem>();
            cic4.IsPromoted.Returns(false);
            cic4.Item.Returns(result.ItemD);
            cic4.Amount.Returns(1);

            #endregion

            result.ScenarioC.Items.Returns(new List<ICartItem>() {cic1, cic2, cic3, cic4});
            result.ScenarioC.Sum.Returns(335); // original value, without promotion

            var cartBPromotion3A = Substitute.For<ICartItem>();
            cartBPromotion3A.Amount.Returns(-20);
            cartBPromotion3A.Item.Returns(result.ItemA);
            cartBPromotion3A.IsPromoted.Returns(true);

            result.Promotion3A.Check(result.ScenarioA).ReturnsNull();
            result.Promotion3A.Check(result.ScenarioB).Returns(cartBPromotion3A);
            result.Promotion3A.Check(result.ScenarioC).Returns(cartBPromotion3A);

            var cartBPromotion2B = Substitute.For<ICartItem>();
            cartBPromotion2B.Amount.Returns(-15 * 2);
            cartBPromotion2B.Item.Returns(result.ItemB);
            cartBPromotion2B.IsPromoted.Returns(true);

            result.Promotion2B.Check(result.ScenarioA).ReturnsNull();
            result.Promotion2B.Check(result.ScenarioB).Returns(cartBPromotion2B);
            result.Promotion2B.Check(result.ScenarioC).Returns(cartBPromotion2B);

            var cartCPromotionCD = Substitute.For<ICartItem>();
            cartCPromotionCD.Amount.Returns(-5);
            cartCPromotionCD.Item.Returns(result.ItemD);
            cartCPromotionCD.IsPromoted.Returns(true);

            result.PromotionCD.Check(result.ScenarioA).ReturnsNull();
            result.PromotionCD.Check(result.ScenarioB).ReturnsNull();
            result.PromotionCD.Check(result.ScenarioC).Returns(cartCPromotionCD);

            return result;
        }

        [TestMethod]
        public void TestScenarioA()
        {
            var context = this.CreatePromotions();
            var promotions = Substitute.For<IPromotions>();

            // No change is expected
            promotions.CheckAll(context.ScenarioA).Returns(context.ScenarioA);

            // Actual tests
            context.Promotion3A.Check(context.ScenarioA).Should().BeNull("No promotion 3A can be applied");
            context.Promotion2B.Check(context.ScenarioA).Should().BeNull("No promotion 2B can be applied");
            context.PromotionCD.Check(context.ScenarioA).Should().BeNull("No promotion CD can be applied");

            var result = promotions.CheckAll(context.ScenarioA);
            result.Sum.Should().Be(100);
            result.Items.Count().Should().Be(3);
            result.Items.Count(item => item.IsPromoted).Should().Be(0);
        }

        [TestMethod]
        public void TestScenarioB()
        {
            var context = this.CreatePromotions();
            var promotions = Substitute.For<IPromotions>();

            var cartBItems = context.ScenarioB.Items.ToList();
            var p3A = context.Promotion3A.Check(context.ScenarioB);
            cartBItems.Add(p3A);
            var p2B = context.Promotion2B.Check(context.ScenarioB);
            cartBItems.Add(p2B);
            var pCD = context.PromotionCD.Check(context.ScenarioB);
            var newCartB = Substitute.For<ICart>();
            newCartB.Items.Returns(cartBItems);
            newCartB.Sum.Returns(370);
            promotions.CheckAll(context.ScenarioB).Returns(newCartB);

            // Actual tests
            p3A.IsPromoted.Should().BeTrue("A promotion for 3A should be generated");
            p3A.Amount.Should().Be(-20);
            p2B.IsPromoted.Should().BeTrue("A promotion for 2B should be generated");
            p2B.Amount.Should().Be(-30);
            pCD.Should().BeNull("Promotion CD is not valid on Scenario B.");

            var result = promotions.CheckAll(context.ScenarioB);
            result.Sum.Should().Be(context.ScenarioB.Sum + p3A.Amount + p2B.Amount);
            result.Items.Count().Should().Be(5);
            result.Items.Count(item => item.IsPromoted).Should().Be(2);
        }

    }
}