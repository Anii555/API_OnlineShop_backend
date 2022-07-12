using Microsoft.AspNetCore.Identity;

namespace ProductsLibrary.DB_Context
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
    }
}
