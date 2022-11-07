namespace ecommerce.Apis.Common.Contrats;

public class Response
{
    public bool Errored { get; set; }
    public string ErrorMessage { get; set; }
}

public sealed class Response<TReturn> : Response
{
    public TReturn Object { get; set; }
}
