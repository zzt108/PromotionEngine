namespace PromotionEngine
{
    public interface IPromotion
    {
        ICartItem Check(ICart cart);
        string Description { get; }
    }
}