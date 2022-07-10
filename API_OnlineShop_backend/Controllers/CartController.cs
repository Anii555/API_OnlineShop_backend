using Microsoft.AspNetCore.Mvc;
using ProductsLibrary;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_OnlineShop_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public CartController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(Cart.Products.Select(x => new CartProductResponce(x.Key, x.Value)));
        }

        // GET api/CartController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cart_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id);
            if (cart_item.Key == null)
            {
                return NotFound();
            }
            return Ok(new CartProductResponce(cart_item.Key, cart_item.Value));
        }

        // POST api/CartController
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var product_item = _productRepository.GetId(id).Result;
            var cart_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id);

            if (cart_item.Key == null || id != cart_item.Key.ProductId)
            {
                Cart.Products.Add(product_item, 1);
                return Ok(new CartProductResponce(product_item, 1));
            }
            else
            {
                int amount = cart_item.Value + 1;

                Cart.Products[cart_item.Key] = amount;
                return Ok(new CartProductResponce(cart_item.Key, amount));
            }
        }

        // PUT api/CartController/5/2
        [HttpPut("{id}/{amount}")]
        public async Task<IActionResult> Put(int id, int amount)
        {
            var change_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id);

            if (change_item.Key != null)
            {
                Cart.Products[change_item.Key] = amount;
            }

            return Ok(new CartProductResponce(change_item.Key, amount));
        }

        // DELETE api/CartController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var del_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id);

            if (del_item.Key != null)
            {
                Cart.Products.Remove(del_item.Key);
            }
            return Ok(Cart.Products.Select(x => new CartProductResponce(x.Key, x.Value)));
        }

        // DELETE api/CartController/
        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            Cart.Products.Clear();

            return Ok(Cart.Products);
        }
    }
}
