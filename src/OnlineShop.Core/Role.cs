using System.Collections.Generic;

namespace OnlineShop.Core
{
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Seller = "Seller";
        public const string Client = "Client";
        public const string AdminOrSeller = Admin + "," + Seller;
        public static readonly IEnumerable<string> All = new[] {Admin, Seller, Client};
    }
}