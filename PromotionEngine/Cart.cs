using System.Collections.Generic;
using System.Linq;
namespace PromotionEngine
{
    /// <summary>
    /// The cart.
    /// </summary>
    public class Cart : ICart
    {
        public Cart(IList<ICartItem> items)
        {
            this.Items = items;
        }

        public IList<ICartItem> Items { get; }

        /// <summary>
        /// Sum of cartItem.Amount * item.UnitPrice in the cart.
        /// </summary>
        public double Sum
        {
            get
            {
                return this.Items.Sum(cartItem =>
                    {
                        if (cartItem.Item != null)
                        {
                            return cartItem.Amount * cartItem.Item.UnitPrice;
                        }
                        else
                        {
                            return cartItem.Amount;
                        }
                    });
            }
        }
    }
}