namespace Application.Services.Abstractions;

public interface IFileData
{
    string FileName { get; }
    long Length { get; }
    Stream OpenReadStream();
}
