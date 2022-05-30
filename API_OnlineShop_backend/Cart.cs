namespace API_OnlineShop_backend
{
    public partial class Cart
    {
        public Cart()
        {
            Products = new HashSet<Product>();
        }
        public int CartItemId { get; set; }
        public int QuantityCartItem { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
