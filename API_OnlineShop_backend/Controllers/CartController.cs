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
        public List<Product>? Get()
        {
            return Cart.Products;
        }

        // GET api/CartController/5
        [HttpGet("{id}")]
        public IEnumerable<Product>? Get(int id)
        {
            return Cart.Products.Where(x=>x.ProductId == id);
        }

        // POST api/CartController
        [HttpPost]
        public void Post(Product product)
        {
            Cart.Products.Add(product);
        }

        // PUT api/CartController/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            Cart.Products.First(x=>x.ProductId == id).QuantityPerUnit = value;
        }

        // DELETE api/CartController/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var del_item = Cart.Products.Where(x=>x.ProductId == id).FirstOrDefault();
            Cart.Products.Remove(del_item);
        }
    }
}
