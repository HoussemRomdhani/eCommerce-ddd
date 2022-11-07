using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.ComponentModel.DataAnnotations;
using ecommerce.Apis.Common;
using eCommerce.Application.Customers.Commands.CreateCustomer;
using eCommerce.Application.Customers.Queries.IsEmailAvailable;
using eCommerce.Application.Customers.Queries.GetCustomerById;
using eCommerce.Application.Customers.Commands.RemoveCustmer;
using eCommerce.Application.Customers.Commands.UpdateCustomer;
using eCommerce.Application.Customers.Commands.CreateCreditCard;
using eCommerce.Application.Customers.Dtos.Requests;
using eCommerce.Application.Customers.Dtos.Responses;
using System.Threading.Tasks;
using System;

namespace ecommerce.Apis.Customers.Controllers
{
    [Route("customers")]
    public sealed class CustomersController : ApiController
    {
        public CustomersController(IMediator mediator) : base(mediator)
        {
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetCustomerByIdQuery(id);

            var result = await Mediator.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CustomerDto customer)
        {
            var command = new CreateCustomerCommand(customer);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [Route("{id}/creditCard")]
        [HttpPost]
        public async Task<IActionResult> AddCreditCard([Required] Guid id, [FromBody] CreateCreditCardRequest dto)
        {
            var command = new CreateCreditCardCommand(id, dto);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Route("email")]
        [HttpGet]
        public async Task<IActionResult> IsEmailAvailable(string email)
        {
            var query = new IsEmailAvailableQuery(email);   
            
            var result = await Mediator.Send(query);

            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Update([Required] Guid id, [FromBody] CustomerDto customer)
        {
            var command = new UpdateCustomerCommand(id, customer);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Remove([Required] Guid id)
        {
            var command = new RemoveCustomerCommand(id);

            var result = await Mediator.Send(command);

            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }

        //    public CreditCardDto Add(Guid customerId, CreditCardDto creditCardDto)
        //    {
        //        ISpecification<Customer> registeredSpec = new CustomerRegisteredSpec(customerId);

        //        Customer customer = _customerRepository.FindOne(registeredSpec);

        //        if (customer == null) throw new Exception("No such customer exists");

        //        var creditCard = CreditCard.Create(customer, creditCardDto.NameOnCard, creditCardDto.CardNumber, creditCardDto.Expiry);

        //        customer.Add(creditCard);

        //        _unitOfWork.Commit();

        //        return _mapper.Map<CreditCard, CreditCardDto>(creditCard);
        //    }
    }
}
