using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsLibrary
{
    public class DataSeed
    {
        public static async Task SeedDataAsync(NorthwindContext context, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<IdentityUser>
                            {
                                new IdentityUser
                                    {
                                        UserName = "TestUserFirst",
                                        Email = "testuserfirst@test.com"
                                    },

                                new IdentityUser
                                    {
                                        UserName = "TestUserSecond",
                                        Email = "testusersecond@test.com"
                                    }
                              };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "qazwsX123@");
                    context.Add(users);
                }
            }
        }
    }
}