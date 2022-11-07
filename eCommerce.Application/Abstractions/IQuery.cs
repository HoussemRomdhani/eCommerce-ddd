using MediatR;

namespace eCommerce.Application.Abstractions;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
