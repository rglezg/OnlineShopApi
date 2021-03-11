using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Core;
using OnlineShop.Core.Entities;
using OnlineShop.Core.Requests;
using OnlineShop.Data;

namespace OnlineShop.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _db;

        public OrdersController(AppDbContext db)
        {
            _db = db;
        }

        [Authorize(Roles = Role.Client)]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Order>> Post([FromBody] OrderAddModel model)
        {
            var (productId, count) = model;
            var product = await _db.Products.FindAsync(productId);
            if (product == null) return NotFound();
            if (product.Count < count)
                return Conflict("The number of items ordered exceeds the existing");
            
            product.Count -= count;
            var order = new Order
            {
                Count = count,
                State = OrderState.Created,
                Date = DateTime.Now.Date,
                ProductId = productId,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            };
            await _db.Orders.AddAsync(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPatch("{id:int}")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Patch))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Patch(int id, [FromBody] OrderEditModel editModel)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound();
            
            order.State = editModel.State;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [Authorize(Roles = Role.Admin)]
        [HttpDelete("{id:int}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _db.Orders.FindAsync(id);
            if (order == null) return NotFound();
            if (order.State == OrderState.Confirmed) 
                return Conflict("Can't delete a confirmed order");
            
            var product = await _db.Products.FindAsync(order.ProductId);
            product.Count += order.Count;
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return Ok(order);
        }
    }
}