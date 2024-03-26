using Api.Common.Constants;
using Api.Common.Controllers;
using Api.Models;
using Application.Services.Seq;
using Application.Services.Seq.Models;
using Common.Constants;
using Contract.Common.Responses;
using Contract.Incoming.Seq.CreateIngestRequest;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Seq;

public class SeqController : ApiController
{
    private readonly ISeqService _seqService;

    public SeqController(ISeqService seqService)
    {
        _seqService = seqService;
    }

    [HttpPost(Endpoints.Seq.Ingest)]
    [Consumes(MediaType.FORM_DATA)]
    [SwaggerResponse(StatusCodes.Status200OK, "Ingest logs to Seq server")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, ErrorMessage.Validation, typeof(ErrorResponse))]
    [SwaggerResponse(StatusCodes.Status502BadGateway, ErrorMessage.BadGateway)]
    public async Task<IActionResult> Ingest([FromForm] CreateIngestRequest request)
    {
        FileData fileData = new(request.File);

        ErrorOr<IngestResult> result = await _seqService.Ingest(fileData);

        return result.Match(
            record => Ok(result.Value),
            errors => Problem(errors));
    }
}
