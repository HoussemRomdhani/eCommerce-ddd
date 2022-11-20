using eCommerce.Domain.SharedKernel.Results;
using MediatR;
using System;

namespace eCommerce.Application.Products.Commands.UpdateProduct;

public sealed class UpdateProductCommand : IRequest<Result>
{
    public Guid Id { get; set; }
    public string Name { get; }
    public decimal Cost { get; }
    public int Quantity { get; }
    public string ProductCodeName { get; }

    private UpdateProductCommand(Guid id, string name, int quantity, decimal cost, string productCodeName)
    {
        Id = id;
        Name = name;
        Cost = cost;
        Quantity = quantity;
        ProductCodeName = productCodeName;
    }

    public static UpdateProductCommand Create(Guid id, string name, int quantity, decimal cost, string productCodeName)
    {
        return new UpdateProductCommand(id, name, quantity, cost, productCodeName);
    }
}
