using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsLibrary.DB_Context
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
    }
}
