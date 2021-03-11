using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Requests;
using OnlineShop.Core.Responses;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        
        [HttpPost("login")] 
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<JwtModel>> Login([FromBody] LoginModel model)  
        {
            var (username, password) = model;
            var user = await _userManager.FindByNameAsync(username);
            if (user == null || !await _userManager.CheckPasswordAsync(user, password)) 
                return Unauthorized();
  
            var claims = new List<Claim>  
            {  
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
            };
            claims.AddRange((await _userManager.GetRolesAsync(user))
                .Select(userRole => new Claim(ClaimTypes.Role, userRole)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
            var token = new JwtSecurityToken(  
                issuer: _configuration["JWT:ValidIssuer"],  
                audience: _configuration["JWT:ValidAudience"],  
                expires: DateTime.Now.AddHours(3),  
                claims: claims,  
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
            );  
  
            return Ok(new JwtModel(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo));
        }
    }
}