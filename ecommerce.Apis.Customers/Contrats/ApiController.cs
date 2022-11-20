using ecommerce.Apis.Customers.Contrats;
using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ecommerce.Apis.Contrats.Customers
{
    [Route("api")]
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

        protected IActionResult Problem(Result result)
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
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: error.Message);
        }
        protected IActionResult Problem<T>(Result<T> result) where T : class
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
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(statusCode: statusCode, title: error.Message);
        }

        protected IActionResult ToResult<T>(Result<T> result) 
        {
            return result.IsSuccess ? Ok(result.Value) : Problem(result);
        }

        protected IActionResult ToResult(Result result)
        {
            return result.IsSuccess ? NoContent() : Problem(result);
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
