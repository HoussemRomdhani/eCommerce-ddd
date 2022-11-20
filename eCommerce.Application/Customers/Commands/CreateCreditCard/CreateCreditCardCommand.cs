using eCommerce.Application.Customers.Dtos.Requests;
using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Customers.Commands.CreateCreditCard;

public sealed class CreateCreditCardCommand : IRequest<Result>
{
    public Guid Id { get;  }
    public string NameOnCard { get; }
    public string CardNumber { get; }
    public DateTime Expiry { get; }

    private CreateCreditCardCommand(Guid id, CreateCreditCardRequest dto)
    {
        Id = id;
        NameOnCard = dto.NameOnCard;
        CardNumber = dto.CardNumber;
        Expiry = dto.Expiry;
    }

    public static CreateCreditCardCommand Create(Guid id, CreateCreditCardRequest dto)
    {
        return new CreateCreditCardCommand(id, dto);
    }
}
