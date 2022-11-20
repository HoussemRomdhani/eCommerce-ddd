using Microsoft.AspNetCore.Mvc;
using MediatR;
using eCommerce.Application.Carts.Queries.GetCart;
using eCommerce.Application.Carts.Commands.RemoveProduct;
using eCommerce.Application.Carts.Commands.Checkout;
using eCommerce.Application.Carts.Commands.AddProduct;
using System.ComponentModel.DataAnnotations;
using eCommerce.Application.Carts.Dtos.Requests;
using ecommerce.Apis.Contrats.Customers;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Application.Carts.Dtos.Responses;

namespace ecommerce.Apis.Cart.Controllers
{
    [Route("cart")]
    public sealed class CartController : ApiController
    {
        public CartController(IMediator mediator) : base(mediator)
        {
        }

        [Route("{customerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(CartResponseDto), 200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Get([Required] Guid customerId)
        {
            return ToResult(await Mediator.Send(GetCustomerCartQuery.Create(customerId)));
        }

        [Route("{customerId}")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Add([Required] Guid customerId, [FromBody] AddProductToCartRequest dto)
        {
            return ToResult(await Mediator.Send(AddProductCommand.Create(customerId, dto)));
        }

        [Route("{customerId}/{productId}")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Remove([Required] Guid customerId, [Required] Guid productId)
        {
            return ToResult(await Mediator.Send(RemoveProductCommand.Create(customerId, productId)));
        }

        [Route("{customerId}")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
        [ProducesResponseType(typeof(Error), 500)]
        public async Task<IActionResult> Checkout([Required] Guid customerId)
        {
            return ToResult(await Mediator.Send(CheckoutCommand.Create(customerId)));
        }
    }
}
