using eCommerce.Domain.SharedKernel.Errors;

namespace eCommerce.Domain.SharedKernel.Results;

public class Result<TValue> : Result
{
    private readonly TValue _value;

    public Result(TValue value, Error error)
        : base(error)
    {
        _value = value;
    }

    public Result(TValue value, List<Error> errors)
       : base(errors)
    {
        _value = value;
    }

    public Result(TValue value): base(new List<Error>())
    {
       _value = value;
    }

    public static implicit operator Result<TValue>(TValue value) => Success(value);

    public TValue Value
    {
        get
        {
          return IsSuccess
         ? _value
          : throw new InvalidOperationException("The value of a failure result can not be accessed.");
        }
    }
}
