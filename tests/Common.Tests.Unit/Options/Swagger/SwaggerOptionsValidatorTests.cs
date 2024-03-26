using Common.Options.Swagger;
using FluentAssertions;

namespace Common.Tests.Unit.Options.Swagger;

public class SwaggerOptionsValidatorTests
{
    private readonly SwaggerOptionsValidator _validator;

    public SwaggerOptionsValidatorTests()
    {
        _validator = new SwaggerOptionsValidator();
    }

    [Theory]
    [InlineData("url", "")]
    [InlineData("", "url")]
    [InlineData("url", "url")]
    public void Validate_ShouldReturnTrue_WhenModelIsValid(string developmentUrl, string productionUrl)
    {
        // Arrange
        var model = new SwaggerOptions
        {
            DevelopmentUrl = developmentUrl,
            ProductionUrl = productionUrl,
        };

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("", "")]
    public void Validate_ShouldReturnFalse_WhenModelIsInvalid(string developmentUrl, string productionUrl)
    {
        // Arrange
        var model = new SwaggerOptions
        {
            DevelopmentUrl = developmentUrl,
            ProductionUrl = productionUrl,
        };

        // Act
        var result = _validator.Validate(model);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
