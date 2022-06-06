using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_OnlineShop_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        Dictionary<int, Product>? Products = new Dictionary<int, Product>();

        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(Cart.Products);
        }

        // GET api/CartController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cart_item = Cart.Products;
            if (!cart_item.Any(x => x.Key == id))
            {
                return NotFound();
            }
            return Ok(cart_item.FirstOrDefault(x => x.Key == id));
        }

        // POST api/CartController
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetById), new { id = product.ProductId}, product);
        }
    }
}
