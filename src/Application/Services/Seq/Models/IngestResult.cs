namespace Application.Services.Seq.Models;

public record IngestResult
{
    public int StatusCode { get; init; }
    public string? Content { get; init; }
}
