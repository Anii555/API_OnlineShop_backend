using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsLibrary;
using System.Collections.Generic;
using System.Linq;

namespace API_OnlineShop_backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getAllProducts")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _productRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            return await _productRepository.GetId(id);
        }
    }
}