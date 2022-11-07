using ecommerce.Apis.Common.Contrats;
using eCommerce.Domain.SharedKernel.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ecommerce.Apis.Common
{
    [Route("api")]
    public class ApiController : ControllerBase
    {
        protected ApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }

        protected IActionResult BadRequest(Error error)
        {
            return BadRequest(new ApiErrorResponse(new[] { error }));
        }

        protected new IActionResult Ok(object value)
        {
            return base.Ok(value);
        }

        protected new IActionResult NotFound()
        {
            return base.NotFound();
        }
    }
}
