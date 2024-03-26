using Application.Services.Abstractions;
using Application.Services.Seq.Models;
using ErrorOr;

namespace Application.Services.Seq
{
    public interface ISeqService
    {
        Task<ErrorOr<IngestResult>> Ingest(IFileData fileData);
    }
}