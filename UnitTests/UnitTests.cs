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

    [TestClass]
    public class UnitTests
    {
        class Context
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

        private Context createPromotions()
        {
            var result = new Context
            {
                ItemA = Substitute.For<IItem>(),
                ItemB = Substitute.For<IItem>(),
                ItemC = Substitute.For<IItem>(),
                ItemD = Substitute.For<IItem>(),

                ScenarioA = Substitute.For<ICart>(),

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

            var cartBPromotion3A = Substitute.For<ICartItem>();
            cartBPromotion3A.Amount.Returns(-20);
            cartBPromotion3A.Item.Returns(result.ItemA);
            cartBPromotion3A.IsPromoted.Returns(true);

            result.Promotion3A.Check(result.ScenarioA).ReturnsNull();
            result.Promotion3A.Check(result.ScenarioB).Returns(cartBPromotion3A);
            result.Promotion3A.Check(result.ScenarioC).Returns(cartBPromotion3A);

            var cartBPromotion2B = Substitute.For<ICartItem>();
            cartBPromotion2B.Amount.Returns(-15);
            cartBPromotion2B.Item.Returns(result.ItemB);
            cartBPromotion2B.IsPromoted.Returns(true);

            result.Promotion2B.Check(result.ScenarioA).ReturnsNull();
            result.Promotion2B.Check(result.ScenarioB).Returns(cartBPromotion2B);
            result.Promotion2B.Check(result.ScenarioC).Returns(cartBPromotion2B);

            var cartCPromotionCD = Substitute.For<ICartItem>();
            cartBPromotion2B.Amount.Returns(-5);
            cartBPromotion2B.Item.Returns(result.ItemD);
            cartBPromotion2B.IsPromoted.Returns(true);

            result.PromotionCD.Check(result.ScenarioA).ReturnsNull();
            result.PromotionCD.Check(result.ScenarioB).ReturnsNull();
            result.PromotionCD.Check(result.ScenarioC).Returns(cartCPromotionCD);

            return result;
        }

        [TestMethod]
        public void Test1()
        {
        }
    }
}