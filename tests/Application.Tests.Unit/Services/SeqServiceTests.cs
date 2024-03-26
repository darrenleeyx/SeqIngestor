using Application.Services.Abstractions;
using Application.Services.Seq;
using Application.Services.Seq.Models;
using Common.Constants;
using Common.Logging;
using ErrorOr;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text;

namespace Application.Tests.Unit.Services;

public class SeqServiceTests
{
    private readonly ISeqService _seqService;
    private readonly ILoggerAdapter<SeqService> _mockLogger;

    public SeqServiceTests()
    {
        var mockHttpClientFactory = CreateMockHttpClientFactory(statusCode: HttpStatusCode.Created);
        _mockLogger = Substitute.For<ILoggerAdapter<SeqService>>();
        _seqService = new SeqService(_mockLogger, mockHttpClientFactory);
    }

    private IHttpClientFactory CreateMockHttpClientFactory(
        HttpStatusCode statusCode, string namedClient = SeqConstants.Name, string baseAddress = "http://localhost:1234",
        string endpoint = SeqConstants.IngestEndpoint, string mediaType = MediaType.JSON)
    {
        var url = $"{baseAddress}/{endpoint}";
        var stringContent = new StringContent(@"{ ""message"": ""hello world"" }", Encoding.UTF8, mediaType);

        var httpHandler = new MockHttpMessageHandler();
        httpHandler
            .When(url)
            .Respond(statusCode, stringContent);

        var httpClient = new HttpClient(httpHandler)
        {
            BaseAddress = new Uri(baseAddress)
        };

        var httpClientFactory = Substitute.For<IHttpClientFactory>();
        httpClientFactory.CreateClient(namedClient).Returns(httpClient);

        return httpClientFactory;
    }

    private IFileData CreateMockFileData()
    {
        var mockFileData = Substitute.For<IFileData>();
        mockFileData.FileName.Returns("file.txt");
        mockFileData.Length.Returns(1000);

        byte[] testData = Encoding.UTF8.GetBytes("Test log data");
        var stream = new MemoryStream(testData);
        mockFileData.OpenReadStream().Returns(stream);

        return mockFileData;
    }

    [Fact]
    public async Task Ingest_ShouldReturnIngestResult_WhenSuccessful()
    {
        // Arrange
        var mockFileData = CreateMockFileData();

        // Act
        ErrorOr<IngestResult> result = await _seqService.Ingest(mockFileData);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.StatusCode.Should().Be((int)HttpStatusCode.Created);
    }

    [Fact]
    public async Task Ingest_ShouldThrowException_WhenSeqServerIsDown()
    {
        // Arrange
        var mockFileData = CreateMockFileData();
        mockFileData.OpenReadStream().Throws<HttpRequestException>(); // simulation

        // Act
        ErrorOr<IngestResult> result = await _seqService.Ingest(mockFileData);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Description.Should().Be(ErrorMessage.BadGateway);
        _mockLogger.Received().LogError(Arg.Any<HttpRequestException>(), Arg.Any<string>());
    }
}
