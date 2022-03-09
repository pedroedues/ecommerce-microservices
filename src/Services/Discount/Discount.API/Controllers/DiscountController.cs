using System;
using System.Net;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Discount.API.Entities;
using Discount.API.Entities.Repositories;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;
        
        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException();
        }
        
        [HttpGet("{productName}", Name = "GetDisount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(
            [FromRoute] string productName)
        {
            var coupon= await _repository.GetDiscount(productName);

            return coupon;
        }
        
        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Coupon>> CreateDiscount(
            [FromBody] Coupon coupon)
        {
            var result = await _repository.CreateDiscount(coupon);
            
            if (!result)
                return BadRequest(coupon);
            
            return CreatedAtAction("CreateDiscount", new { productName = coupon.ProductName }, coupon);
        }
        
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Coupon>> UpdateDiscount(
            [FromBody] Coupon coupon)
        {
            var result = await _repository.UpdateDiscount(coupon);

            if (!result)
                return BadRequest(coupon);
            
            return NoContent();
        }
        
        [HttpDelete("{productName}", Name = "DeleteDiscount")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProductById(
            [FromRoute] string productName)
        {
            var deleted = await _repository.DeleteDiscount(productName);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}