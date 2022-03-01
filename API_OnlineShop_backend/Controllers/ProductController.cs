using Microsoft.AspNetCore.Mvc;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
    //    private static readonly string[] Summaries = new[]
    //       {
    //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    //};

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProduct")]
        public IEnumerable<Product> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Product
            {
                ProductId = Random.Shared.Next(0, 55),
                //ProductName = Random.Shared.Next(0,55),
                UnitPrice = Random.Shared.Next(0, 55)
            })
            .ToArray();
        }
    }
}