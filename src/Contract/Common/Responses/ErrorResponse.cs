namespace Contract.Common.Responses;

public record ErrorResponse
{
    public string Message { get; init; } = null!;
    public Dictionary<string, List<string>> Errors { get; init; } = new();
}
