namespace PromotionEngine
{
    using System;
    using System.Linq;

    public class Promotion3A : IPromotion
    {
        public Promotion3A()
        {
            this.Description = "3 A's for 130";
        }

        public ICartItem Check(ICart cart)
        {
            var sumOfA = cart.Items.Where(item => item.Item.SKU == "A" && item.IsPromoted == false ).Sum(cartItem => cartItem.Amount);
            if (sumOfA >= 3)
            {
                int multiplier = Convert.ToInt32(sumOfA) / 3;
                return new CartItem(null, -20 * multiplier, this);
            }
            else
            {
                return null;
            }
        }

        public string Description { get; }
    }
}