namespace PromotionEngine
{
    public class CartItem : ICartItem
    {
        public CartItem(IItem item, double amount)
            : this(item, amount, null)
        {
        }

        public CartItem(IItem item, double amount, IPromotion promotion)
        {
            this.Item = item;
            this.Amount = amount;
            this.Promotion = promotion;
        }

        public IItem Item { get; }

        public double Amount { get; }

        public bool IsPromoted => this.Promotion != null;

        public IPromotion Promotion { get; set; }
    }
}