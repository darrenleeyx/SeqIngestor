using Application.Services.Abstractions;

namespace Api.Models;

public class FileData : IFileData
{
    private readonly IFormFile _formFile;

    public FileData(IFormFile formFile)
    {
        _formFile = formFile;
    }

    public string FileName => _formFile.FileName;

    public long Length => _formFile.Length;

    public Stream OpenReadStream()
    {
        return _formFile.OpenReadStream();
    }
}
