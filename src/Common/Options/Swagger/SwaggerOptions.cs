using System.Text.Json.Serialization;

namespace Common.Options.Swagger;

public record SwaggerOptions
{
    public const string SectionName = "Swagger";

    [JsonPropertyName("DevelopmentUrl")]
    public string DevelopmentUrl { get; init; } = null!;

    [JsonPropertyName("ProductionUrl")]
    public string ProductionUrl { get; init; } = null!;
}
