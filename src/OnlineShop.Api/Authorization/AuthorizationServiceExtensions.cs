using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Api.Authorization;
using OnlineShop.Core.Entities;

namespace OnlineShop.Api.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static async Task<bool> AuthorizeEditAsync(
            this IAuthorizationService authorizationService,
            ClaimsPrincipal user,
            IOwnedResource resource) =>
            (await authorizationService.AuthorizeAsync(user, resource, Policy.Edit)).Succeeded;
    }
}