namespace PromotionEngine
{
    using System.Collections.Generic;

    public interface ICart
    {
        IEnumerable<ICartItem> Items { get; }
    }
}