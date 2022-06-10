using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_OnlineShop_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(Cart.Products);
        }

        // GET api/CartController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var cart_item = Cart.Products;
            if (!cart_item.Any(x => x.Key.ProductId == id))
            {
                return NotFound();
            }
            return Ok(cart_item.FirstOrDefault(x => x.Key.ProductId == id));
        }

        // POST api/CartController
        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var cart_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id);

            if (cart_item.Key != null)
            {
                Cart.Products.Add(cart_item.Key, cart_item.Value);
                return Ok("Товар добавлен в корзину");
            }

            if (id == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), new { id = cart_item }, Cart.Products);
        }

        // PUT api/CartController/5/2
        [HttpPut("{id}/{quantity}")]
        public async Task<IActionResult> Put(int id, int quantity)
        {
            var change_item = Cart.Products.FirstOrDefault(x => x.Key.ProductId == id).Key;

            if (change_item != null)
            {
                Cart.Products[change_item] = quantity;
            }

            return Ok();
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
            return Ok(Cart.Products);
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
