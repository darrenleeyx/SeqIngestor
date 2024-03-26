using Application.Services.Abstractions;
using Application.Services.Seq.Models;
using Common.Constants;
using Common.Logging;
using ErrorOr;
using System.Text;

namespace Application.Services.Seq;

public class SeqService : ISeqService
{
    private readonly ILoggerAdapter<SeqService> _logger;
    private readonly HttpClient _client;

    public SeqService(
        ILoggerAdapter<SeqService> logger,
        IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient(SeqConstants.Name);
        _logger = logger;
    }

    public async Task<ErrorOr<IngestResult>> Ingest(IFileData fileData)
    {
        try
        {
            using (StreamReader stream = new(fileData.OpenReadStream()))
            {
                string logs = await stream.ReadToEndAsync();

                StringContent content = new(logs, Encoding.UTF8, MediaType.CLEF);

                HttpResponseMessage response = await _client.PostAsync(SeqConstants.IngestEndpoint, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                return new IngestResult
                {
                    StatusCode = (int)response.StatusCode,
                    Content = responseContent
                };
            }
        }
        catch (HttpRequestException exception)
        {
            _logger.LogError(exception, exception.Message);
            return Errors.BadGateway();
        }
    }
}
