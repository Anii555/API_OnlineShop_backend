using System;
using System.Collections.Generic;

namespace API_OnlineShop_backend
{
    public partial class OrderSubtotal
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
