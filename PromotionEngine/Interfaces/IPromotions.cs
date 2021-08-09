namespace PromotionEngine
{
    using System.Collections.Generic;

    public interface IPromotions
    {
        IEnumerable<IPromotion> PromotionCollection { get; }
        ICart CheckAll(ICart cart);
    }
}