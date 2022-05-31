namespace API_OnlineShop_backend
{
    public static class Cart
    {
        public static int CartItemId { get; set; }

        public static int QuantityCartItem { get; set; }

        public static List<Product>? Products { get; set; }
    }
}
