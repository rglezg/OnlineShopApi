using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Extensions;
using OnlineShop.Core;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Requests;
using OnlineShop.Data;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;
        private readonly IAuthorizationService _authorizationService;

        public ProductsController(AppDbContext db, IAuthorizationService authorizationService)
        {
            _db = db;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] string ownerId)
        {
            if (ownerId == null)
            {
                return Ok(await _db.Products.ToListAsync());
            }
            var owner = await _db.Users
                .Include(u => u.Products)
                .FirstOrDefaultAsync(u => u.Id == ownerId);
            if (owner == null) return NotFound();
            return Ok(owner.Products);
        }
        
        [HttpGet("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await _db.Products.FindAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        [Authorize(Roles = Role.AdminOrSeller)]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] ProductModel model)
        {
            var product = new Product();
            model.Patch(product);
            product.OwnerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await _db.Products.AddAsync(product);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new {id = product.Id},product);
        }

        [Authorize(Roles = Role.AdminOrSeller)]
        [HttpPatch("{id:int}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Product>> Patch(int id, [FromBody] ProductModel model)
        {
            var product = await _db.Products.FindAsync(id);
            model.Patch(product);
            if (product == null) return NotFound();
            if (!await _authorizationService.AuthorizeEditAsync(User, product)) return Forbid();
            
            await _db.SaveChangesAsync();
            return Ok(product);
        }

        [Authorize(Roles = Role.AdminOrSeller)]
        [HttpDelete("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null) return NotFound();
            if (!await _authorizationService.AuthorizeEditAsync(User, product)) return Forbid();

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return Ok(product);
        }
    }
}