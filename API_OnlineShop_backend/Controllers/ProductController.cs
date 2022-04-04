using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_OnlineShop_backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        public NorthwindContext db_context;

        public ProductController(NorthwindContext context)
        {
            db_context = context;
        }

        [HttpPost(Name = "GetAllProduct")]
        public async Task<IEnumerable<Product>> Get()
        {
            return await db_context.Products.ToListAsync();
        }

        [HttpGet("{id}")]//(Name = "GetProductID")]
        public async Task<IEnumerable<Product>> Buy(int? id)
        {
            //if (id == null) return RedirectToAction("Index");
            var rez = db_context.Products.Where(x => x.ProductId == id);
            return await rez.ToListAsync();
        }

        //public ProductController(NorthwindContext context)
        //{
        //    db = context;
        //    if (!db.Products.Any())
        //    {
        //        db.Products.Add(new Product { ProductId = 5, ProductName = "Apple", UnitPrice = 79900 });
        //        db.Products.Add(new Product { ProductId = 10, ProductName = "Samsung", UnitPrice = 49900 });
        //        db.Products.Add(new Product { ProductId = 21, ProductName = "Google", UnitPrice = 52900 });
        //        db.SaveChanges();
        //    }
        //}

        //private readonly ILogger<ProductController> _logger;

        //public ProductController(ILogger<ProductController> logger)
        //{
        //    _logger = logger;
        //}

        //    var entity = db_context.Model.FindEntityType(typeof(Product).FullName);
        //    //return db.Products.ToList();
        //    return Enumerable.Range(1, 5).Select(index => new Product
        //    {
        //        ProductId = index,
        //        ProductName = entity.Name,// db_context.Products.Where(x => x.ProductName.Contains("Chef")),
        //        UnitPrice = Random.Shared.Next(0, 55)
        //    })
        //        .ToList();
        ////.ToArray();
        //}

        //    public IActionResult Get()
        //    {
        //        var entity = db_context.Model
        //.FindEntityType(typeof(Product).FullName);

        //        var tableName = entity.GetTableName();
        //        var schemaName = entity.GetSchema();
        //        var key = entity.FindPrimaryKey();
        //        var properties = entity.GetProperties();

        //        var products = db_context.Products
        //            .AsNoTracking()
        //               .Where(s => s.ProductId > 70)
        //               .ToList();

        //        return Ok(products);
        //    }


    }
}