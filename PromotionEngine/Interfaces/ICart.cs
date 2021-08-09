namespace PromotionEngine
{
    using System.Collections.Generic;

    public interface ICart
    {
        IList<ICartItem> Items { get; }
        double Sum { get; }
    }
}