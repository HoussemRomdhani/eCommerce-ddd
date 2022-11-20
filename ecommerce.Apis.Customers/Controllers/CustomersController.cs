using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.ComponentModel.DataAnnotations;
using eCommerce.Application.Customers.Commands.CreateCustomer;
using eCommerce.Application.Customers.Queries.GetCustomerById;
using eCommerce.Application.Customers.Commands.RemoveCustmer;
using eCommerce.Application.Customers.Commands.UpdateCustomer;
using eCommerce.Application.Customers.Commands.CreateCreditCard;
using eCommerce.Application.Customers.Dtos.Requests;
using eCommerce.Application.Customers.Dtos.Responses;
using eCommerce.Application.Customers.Queries.GetAllCustomers;
using eCommerce.Domain.SharedKernel.Errors;
using ecommerce.Apis.Contrats.Customers;

namespace ecommerce.Apis.Customers.Controllers;

[Route("customers")]
public sealed class CustomersController : ApiController
{
    public CustomersController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CustomerResponseDto>), 200)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> GetAll()
    {
        return ToResult(await Mediator.Send(GetAllCustomersQuery.Create));
    }

    [Route("{id}")]
    [HttpGet]
    [ProducesResponseType(typeof(CustomerResponseDto), 200)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> GetById([Required] Guid id)
    {
       return ToResult(await Mediator.Send(GetCustomerByIdQuery.Create(id)));
    }

    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> Add([FromBody] CreateCustomerRequestDto dto)
    {
        return ToResult(await Mediator.Send(CreateCustomerCommand.Create(dto)));
    }

    [Route("{id}")]
    [HttpPut]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> Update([Required] Guid id, [FromBody] UpdateCustomerRequestDto dto)
    {
        return ToResult(await Mediator.Send(UpdateCustomerCommand.Create(id, dto)));
    }

    [Route("{id}")]
    [HttpDelete]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> Remove([Required] Guid id)
    {
        return ToResult(await Mediator.Send(RemoveCustomerCommand.Create(id)));
    }

    [Route("{id}/creditCard")]
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(IEnumerable<Error>), 400)]
    [ProducesResponseType(typeof(Error), 500)]
    public async Task<IActionResult> AddCreditCard([Required] Guid id, [FromBody] CreateCreditCardRequest dto)
    {
        return ToResult(await Mediator.Send(CreateCreditCardCommand.Create(id, dto)));
    }
}
