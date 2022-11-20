namespace eCommerce.Domain.SharedKernel.Errors;

public enum ErrorType
{
    Validation,
    Conflict,
    NotFound
}

public sealed class Error : ValueObject
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    private Error(ErrorType type, string code, string message)
    {
        Type = type;
        Code = code;
        Message = message;
    }

    public string Code { get; }

    public string Message { get; }

    public ErrorType? Type { get; }

    public static implicit operator string(Error error) => error?.Code ?? string.Empty;
    public static Error Validation(string code, string message) => new Error(ErrorType.Validation, code, message);
    public static Error Conflict(string code, string message) => new Error(ErrorType.Conflict, code, message);
    public static Error NotFound(string code, string message) => new Error(ErrorType.NotFound, code, message);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Code;
        yield return Message;
    }

    internal static Error None => new(string.Empty, string.Empty);
}
