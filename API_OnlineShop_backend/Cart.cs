using Microsoft.AspNetCore.Mvc;
using ProductsLibrary;

namespace API_OnlineShop_backend
{
    public record CartProductResponce(Product Product, int Amount);

    public static class Cart
    {
        public static int CartItemId { get; set; }

        public static Dictionary<Product, int> Products = new Dictionary<Product, int>();
    }
}
