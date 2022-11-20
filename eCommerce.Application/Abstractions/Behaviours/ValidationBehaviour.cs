using eCommerce.Domain.SharedKernel.Errors;
using eCommerce.Domain.SharedKernel.Results;
using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace eCommerce.Application.Abstractions.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : 
             IPipelineBehavior<TRequest, TResponse>
             where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;
    public ValidationBehaviour(IValidator<TRequest> validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(TRequest request, 
                                          RequestHandlerDelegate<TResponse> next, 
                                          CancellationToken cancellationToken)
    {
        if (_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors.ConvertAll(validationFailure => Error.Validation(validationFailure.PropertyName, validationFailure.ErrorMessage));

        return (dynamic)Result.Failure<Guid>(errors);
    }
}
