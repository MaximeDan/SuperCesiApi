namespace SuperCesiApi.Services;

public class ActionResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    
    public Object? Object { get; set; }

    public ActionResponse(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }

    public ActionResponse(int statusCode)
    {
        StatusCode = statusCode;
    }
    
    public ActionResponse(int statusCode, Object obj)
    {
        StatusCode = statusCode;
        Object = obj;
    }
}
