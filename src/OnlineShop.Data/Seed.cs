using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core;
using OnlineShop.Core.Entities;

namespace OnlineShop.Data
{
    internal static class Seed
    {
        private static readonly PasswordHasher<AppUser> PasswordHasher = new ();
        
        private static string NewId() => Guid.NewGuid().ToString();

        private static readonly IReadOnlyDictionary<string, string> RoleIds =
            Role.All.ToDictionary(role => role, _ => NewId());
        
        private static readonly string AdminUserId = NewId();
        private static readonly string SellerUserId = NewId();
        private static readonly string ClientUserId = NewId();

        private static readonly IEnumerable<(string,string,string)> UserData = new[]
        {
            (AdminUserId, "admin", "Qwerty*1234"),
            (SellerUserId, "seller", "Asdfg*1234"),
            (ClientUserId, "client", "Zxcvb*1234")
        };

        private static readonly IEnumerable<AppUser> Users =
            UserData.Select(data =>
            {
                var (id, name, password) = data;
                var email = $"{name}@mail.com";
                return new AppUser
                {
                    Id = id,
                    UserName = name,
                    Email = email,
                    EmailConfirmed = true,
                    PasswordHash = PasswordHasher.HashPassword(null, password),
                    NormalizedUserName = name.ToUpper(),
                    NormalizedEmail = email.ToUpper(),
                    SecurityStamp = string.Empty
                };
            });

        private static readonly IEnumerable<IdentityRole> Roles =
            Role.All.Select(role => new IdentityRole
            {
                Id = RoleIds[role],
                Name = role,
                NormalizedName = role
            });

        private static readonly IEnumerable<IdentityUserRole<string>> UserRoles = new[]
        {
            new IdentityUserRole<string> {RoleId = RoleIds[Role.Admin], UserId = AdminUserId},
            new IdentityUserRole<string> {RoleId = RoleIds[Role.Seller], UserId = SellerUserId},
            new IdentityUserRole<string> {RoleId = RoleIds[Role.Client], UserId = ClientUserId},
        };

        public static void SeedData(this ModelBuilder builder)
        {
            builder.Entity<AppUser>().HasData(Users);
            builder.Entity<IdentityRole>().HasData(Roles);
            builder.Entity<IdentityUserRole<string>>().HasData(UserRoles);
        }
    }
}