namespace PromotionEngine
{
    public interface ICartItem
    {
        IItem Item { get; }
        double Amount { get; }
        bool IsPromoted { get;}
        IPromotion Promotion { get; set; }
    }

    public interface IItem
    {
        string SKU { get; }
        double UnitPrice { get; }
    }
}