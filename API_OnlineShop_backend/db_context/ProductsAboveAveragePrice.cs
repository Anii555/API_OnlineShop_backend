using System;
using System.Collections.Generic;

namespace API_OnlineShop_backend
{
    public partial class ProductsAboveAveragePrice
    {
        public string ProductName { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
    }
}
