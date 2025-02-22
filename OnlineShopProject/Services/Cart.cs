namespace OnlineShopProject.Services
{
    public class Cart
    {
        private List<CartItem> items = new List<CartItem>();

        public IReadOnlyList<CartItem> Items { get { return items; } }

        public virtual void AddItem(Product product, int quantity)
        {
            CartItem? line = items.
                Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (line is null)
            {
                items.Add(new CartItem
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
            => items.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public decimal ComputeTotalValue()
            => items.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => items.Clear();
    }
}
