﻿using Microsoft.AspNetCore.Mvc;

namespace API_OnlineShop_backend
{
    public static class Cart
    {
        public static int CartItemId { get; set; }

        public static Dictionary<Product, int> Products { get; set; }
    }
}
