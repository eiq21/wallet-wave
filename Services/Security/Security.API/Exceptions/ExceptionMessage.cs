namespace Security.API.Exceptions;
public class ExceptionMessage
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    public string StackTrace { get; set; } = null!;
}
