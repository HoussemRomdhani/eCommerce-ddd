using eCommerce.Domain.SharedKernel.Errors;

namespace eCommerce.Domain.SharedKernel.Results;

public class Result 
{
    private List<Error> _errors = new();
    protected Result(List<Error> errors)
    {
        _errors.AddRange(errors);
    }

    protected Result(Error error)
    {
        _errors.Add(error);
    }

    public bool IsSuccess
    {
        get
        {
            return !IsFailure;
        }
    }

    public bool IsFailure
    {
        get
        {
          return  _errors is not null &&
                  _errors.Count > 0;
        }
    }

    public List<Error> Errors => _errors;
    public Error Error => _errors.FirstOrDefault();

    public static Result Success() => new(new List<Error>());
    public static Result Failure(Error error) => new(error);
    public static Result Failure(List<Error> errors) => new(errors);

    public static Result<TValue> Success<TValue>(TValue value) => new(value);
    public static Result<TValue> Failure<TValue>(Error error) => new(default!, error);
    public static Result<TValue> Failure<TValue>(List<Error> errors) => new(default!, errors);

}
