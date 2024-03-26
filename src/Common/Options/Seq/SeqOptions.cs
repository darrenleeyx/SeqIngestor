using System.Text.Json.Serialization;

namespace Common.Options.Seq;

public record SeqOptions
{
    public const string SectionName = "Seq";

    [JsonPropertyName("ServerUrl")]
    public string ServerUrl { get; init; } = null!;

    [JsonPropertyName("ApiKey")]
    public string ApiKey { get; init; } = null!;
}