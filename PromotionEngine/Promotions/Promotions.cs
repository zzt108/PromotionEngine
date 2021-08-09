namespace PromotionEngine
{
    using System.Collections.Generic;

    public class Promotions : IPromotions
    {
        public Promotions(IEnumerable<IPromotion> promotionCollection)
        {
            this.PromotionCollection = promotionCollection;
        }

        public IEnumerable<IPromotion> PromotionCollection { get; }

        public ICart CheckAll(ICart cart)
        {
            foreach (var promotion in PromotionCollection)
            {
                var promotionCartItem = promotion.Check(cart);
                if (promotionCartItem != null)
                {
                    cart.Items.Add(promotionCartItem);
                }
            }

            return cart;
        }
    }
}