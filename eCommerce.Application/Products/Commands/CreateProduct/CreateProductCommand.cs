using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : IRequest<Result<Guid>>
{
    public string Name { get;}
    public decimal Cost { get; }
    public int Quantity { get;}
    public string ProductCodeName { get;}

    private CreateProductCommand(string name, int quantity, decimal cost, string productCodeName)
    {
        Name = name;
        Cost = cost;
        Quantity = quantity;
        ProductCodeName = productCodeName;
    }

    public static CreateProductCommand Create(string name, int quantity, decimal cost, string productCodeName) => 
               new CreateProductCommand(name, quantity, cost, productCodeName);
}
