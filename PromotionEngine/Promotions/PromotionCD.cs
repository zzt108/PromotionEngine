namespace PromotionEngine
{
    using System;
    using System.Linq;

    public class PromotionCD : IPromotion
    {
        public PromotionCD()
        {
            this.Description = "C and D for 30";
        }

        public ICartItem Check(ICart cart)
        {
            var sumC = cart.Items.Where(item => !item.IsPromoted && item.Item.SKU == "C").Sum(cartItem => cartItem.Amount);
            var sumD = cart.Items.Where(item => !item.IsPromoted && item.Item.SKU == "D").Sum(cartItem => cartItem.Amount);
            var multiplier = Math.Min(sumC, sumD);
            if (multiplier >= 1)
            {
                return new CartItem(null, -5 * multiplier, this);
            }
            else
            {
                return null;
            }
        }

        public string Description { get; }
    }
}