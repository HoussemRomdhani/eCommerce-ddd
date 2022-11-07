using eCommerce.Application.Abstractions;
using eCommerce.Application.Customers.Dtos.Requests;
using eCommerce.Domain.SharedKernel.Results;
using System;

namespace eCommerce.Application.Customers.Commands.CreateCreditCard;

public class CreateCreditCardCommand : ICommand<Result>
{
    public Guid CustomerId { get;  }
    public CreateCreditCardRequest CreditCard { get; }

    public CreateCreditCardCommand(Guid id, CreateCreditCardRequest dto)
    {
        CustomerId = id;
        CreditCard = dto;
    }
}
