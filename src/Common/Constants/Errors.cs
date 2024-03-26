using ErrorOr;

namespace Common.Constants;

public enum CustomErrorType
{
    Failure,
    Unexpected,
    Validation,
    Conflict,
    NotFound,
    Unauthorized,
    Forbidden,
    BadGateway
}

public static class Errors
{
    public static Error BadGateway() =>
        Error.Custom(
            type: (int)CustomErrorType.BadGateway,
            code: "Bad Gateway",
            description: ErrorMessage.BadGateway);
}
