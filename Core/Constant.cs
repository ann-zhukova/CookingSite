namespace Core;

public class Constants
{
    public const int UserNameLength = 64;
    public const int EmailLength = 64;
    public const int PasswordLength = 128;
    public const int MinGridHeight = 20;
    public const int MaxGridHeight = 60;
    public const int MinGridWidth = 10;
    public const int MaxGridWidth = 50;
    public const int MinSpeed = 2;
    public const int MaxSpeed = 5;
    public const int MinLoginLength = 4;
    public const int MaxLoginLength = 20;
    public const int MinPasswordLength = 8;
    public const int MaxPasswordLength = 20;
    public const int MinEmailLength = 5;
    public const int MaxEmailLength = 60;
    
    public static class ErrorCode
    {
        public const string Unknown = "unknown";
        public const string BadRequest = "bad-request";
        public const string Unauthorized = "unauthorized";
        public const string Forbidden = "forbidden";
        public const string NotFound = "notfound";
        public const string Conflict = "conflict";
    }
    
    public static class ClaimsNames
    {
        public const string UserId = "UserId";
        public const string UserRole = "UserRole";
    }
}