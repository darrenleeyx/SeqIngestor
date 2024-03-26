using Microsoft.AspNetCore.Http;

namespace Contract.Incoming.Seq.CreateIngestRequest;

public record CreateIngestRequest
{
    public IFormFile File { get; init; } = null!;
}