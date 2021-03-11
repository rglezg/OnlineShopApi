using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using OnlineShop.Core;
using OnlineShop.Core.Entities;

namespace OnlineShop.Api.Authorization
{
    public record OwnedResourceRequirement : IAuthorizationRequirement; 
    
    public class OwnedResourceAuthorizationHandler
    : AuthorizationHandler<OwnedResourceRequirement, IOwnedResource>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            OwnedResourceRequirement requirement,
            IOwnedResource resource)
        {
            if (context.User.IsInRole(Role.Admin)
                || context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value == resource.OwnerId)
                context.Succeed(requirement);
            return Task.CompletedTask;
        }
    }
}