namespace Domain.Model.Interfaces.Infrastructure;

public interface IBlobService
{
    Task<string> UploadAsync(Stream stream);
    Task DeleteAsync(string BlobName);
}
