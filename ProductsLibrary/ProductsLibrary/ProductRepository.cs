using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsLibrary
{
    public class ProductRepository : IdentityDbContext<IdentityUser, IdentityRole, string> //Guid
    {
        private readonly NorthwindContext _context;

        public ProductRepository(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetId(int id)
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
