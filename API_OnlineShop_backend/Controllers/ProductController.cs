using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsLibrary;
using System.Collections.Generic;
using System.Linq;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository productRepository;

        [HttpGet("getAllProducts")]  
        public async Task<IEnumerable<Product>> Get()
        {
            return await productRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await productRepository.GetId(id);
        }
    }
}