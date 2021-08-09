namespace PromotionEngine
{
    using System;
    using System.Linq;

    public class Promotion2B : IPromotion
    {
        public Promotion2B()
        {
            this.Description = "2 B's for 45";
        }

        public ICartItem Check(ICart cart)
        {
            var sum = cart.Items.Where(item => !item.IsPromoted && item.Item.SKU == "B")
                .Sum(cartItem => cartItem.Amount);
            if (sum >= 2)
            {
                int multiplier = Convert.ToInt32(sum) / 2;
                return new CartItem(null, -15 * multiplier, this);
            }
            else
            {
                return null;
            }
        }

        public string Description { get; }
    }
}