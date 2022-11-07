using MediatR;

namespace eCommerce.Application.Abstractions;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
     where TCommand : ICommand<TResponse>
{
}
