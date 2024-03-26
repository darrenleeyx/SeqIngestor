using Common.Options.Seq;
using FluentAssertions;

namespace Common.Tests.Unit.Options.Seq;

public class SeqOptionsValidatorTests
{
    private readonly SeqOptionsValidator _validator;

    public SeqOptionsValidatorTests()
    {
        _validator = new SeqOptionsValidator();
    }

    [Fact]
    public void Validate_ShouldReturnTrue_WhenModelIsValid()
    {
        // Arrange
        var model = new SeqOptions
        {
            ServerUrl = "url",
            ApiKey = "key",
        };

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("url", "")]
    [InlineData("", "key")]
    [InlineData("", "")]
    public void Validate_ShouldReturnFalse_WhenModelIsInvalid(string serverUrl, string apiKey)
    {
        // Arrange
        var model = new SeqOptions
        {
            ServerUrl = serverUrl,
            ApiKey = apiKey,
        };

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
