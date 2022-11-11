using eCommerce.Application.Abstractions;
using eCommerce.Domain.Products;
using eCommerce.Domain.SharedKernel.Results;

namespace eCommerce.Application.Products.Commands.CreateProduct;

public sealed class CreateProductCommand : ICommand<Result>
{
    public string Name { get;}
    public int Quantity { get;}
    public decimal Cost { get;}
    public ProductCode ProductCode { get;}

    public CreateProductCommand(string name, int quantity, decimal cost, ProductCode productCode)
    {
        Name = name;
        Quantity = quantity;
        Cost = cost;
        ProductCode = productCode;
    }
}
