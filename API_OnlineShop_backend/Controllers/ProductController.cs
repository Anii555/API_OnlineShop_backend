using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public ProductController(NorthwindContext context)
        {
            _context = context;
        }

        [HttpGet("getAllProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            var prod = await _context.Products.FirstOrDefaultAsync(x => x.ProductId == id);

            if (prod == null) 
            { 
                return null; 
            }
            else 
            { 
                return prod; 
            }
        }
    }
}