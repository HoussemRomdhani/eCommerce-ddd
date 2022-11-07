using Microsoft.AspNetCore.Mvc;
using MediatR;
using eCommerce.Application.Carts.Queries.GetCart;
using eCommerce.Application.Carts.Commands.RemoveProduct;
using eCommerce.Application.Carts.Commands.Checkout;
using eCommerce.Application.Carts.Commands.AddProduct;
using System.ComponentModel.DataAnnotations;
using eCommerce.Application.Carts.Dtos.Requests;
using ecommerce.Apis.Common;

namespace ecommerce.Apis.Cart.Controllers
{
    [Route("cart")]
    public sealed class CartController : ApiController
    {
        public CartController(IMediator mediator)
            :base(mediator)
        {
        }

        [Route("{customerId}")]
        [HttpGet]
        public async Task<IActionResult> Get([Required] Guid customerId)
        {
            var query = new GetCustomerCartQuery(customerId);

            var result = await Mediator.Send(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Route("{customerId}")]
        [HttpPut]
        public async Task<IActionResult> Add([Required] Guid customerId, [FromBody] AddProductToCartRequest cart)
        {
            var command = new AddProductCommand(customerId, cart);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Route("{customerId}/{productId}")]
        [HttpDelete]
        public async Task<IActionResult> Remove([Required] Guid customerId, [Required] Guid productId)
        {
            var command = new RemoveProductCommand(customerId, productId);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Route("{customerId}")]
        [HttpPost]
        public async Task<IActionResult> Checkout([Required] Guid customerId)
        {
            var command = new CheckoutCommand(customerId);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }
}
