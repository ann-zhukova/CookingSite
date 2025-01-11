namespace Front.Models;

public abstract class BaseResponseJsModel
{
    public string ErrorCode { get; set; }
    
    public string ErrorDetail { get; set; }
}