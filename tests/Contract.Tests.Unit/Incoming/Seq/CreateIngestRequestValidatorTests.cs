using Contract.Incoming.Seq.CreateIngestRequest;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Contract.Tests.Unit.Incoming.Seq;

public class CreateIngestRequestValidatorTests
{
    private CreateIngestRequestValidator _validator;
    public CreateIngestRequestValidatorTests()
    {
        _validator = new CreateIngestRequestValidator();
    }

    [Fact]
    public void Validate_ShouldBeTrue_WhenRequestIsValid()
    {
        // Arrange
        var mockFile = Substitute.For<IFormFile>();
        mockFile.FileName.Returns("file.txt");
        mockFile.Length.Returns(1000);

        var request = new CreateIngestRequest
        {
            File = mockFile
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();

    }

    [Fact]
    public void Validate_ShouldBeFalse_WhenRequestIsInvalid()
    {
        // Arrange
        var request = new CreateIngestRequest();

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
