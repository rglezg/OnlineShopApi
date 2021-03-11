using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public IEnumerable<Product> Products { get; set; }
    }
}