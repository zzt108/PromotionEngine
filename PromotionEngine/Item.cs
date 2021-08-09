namespace PromotionEngine
{
    public class Item : IItem
    {
        public Item(string sku, double unitPrice)
        {
            this.SKU = sku;
            this.UnitPrice = unitPrice;
        }

        public string SKU { get; }

        public double UnitPrice { get; }
    }
}