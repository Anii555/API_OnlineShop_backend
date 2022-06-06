using Microsoft.AspNetCore.Mvc;

namespace API_OnlineShop_backend
{
    public class Cart
    {
        public static int CartItemId { get; set; }

        public static Dictionary<int, Product>? Products { get; set; }
    }
}
