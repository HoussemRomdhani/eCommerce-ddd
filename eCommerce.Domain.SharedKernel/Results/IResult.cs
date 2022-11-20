namespace eCommerce.Domain.SharedKernel.Results;

public interface IResult
{
    bool IsFailure { get; }
    bool IsSuccess { get; }
}
