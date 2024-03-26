namespace Common.Constants;

public class ErrorMessage
{
    public const string Validation = "One or more validation errors have occurred.";
    public const string InvalidJson = "Unable to parse JSON. Please ensure that the JSON string is in valid format.";
    public const string NotFound = "The server is unable to find the requested resource.";
    public const string Unauthorized = "You do not have the permission to access this service.";
    public const string Forbidden = "You do not have the permission to access this service.";
    public const string Conflict = "The server is unable to fulfill the request because there is a conflict with the resource.";
    public const string InternalServerError = "An unexpected server error has occurred. Please contact the administrator for more information.";
    public const string BadGateway = "The server, while acting as a gateway or proxy, received an invalid response from the upstream server.";
}

