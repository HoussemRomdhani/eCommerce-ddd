using ecommerce.Apis.Common.Contrats;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace ecommerce.Apis.Common
{
    [Route("api")]
    [Controller]
    public abstract class ApiController : ControllerBase
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

        protected IActionResult Ko(Result result)  
        {
            if (result.Errors.All(x => x.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new ModelStateDictionary();

                foreach (var item in result.Errors)
                    modelStateDictionary.AddModelError(item.Code, item.Message);

                return ValidationProblem(modelStateDictionary);
            }


            var error = result.Error;

            var statusCode = error.Type switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return base.Problem();
        }

        protected IActionResult Success(object result)
        {
            return Ok(result);
        }


        protected IActionResult BadRequest(List<Error> errors)
        {
            return BadRequest(new ApiErrorResponse(errors));
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
