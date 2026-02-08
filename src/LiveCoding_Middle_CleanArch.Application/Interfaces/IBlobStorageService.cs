namespace LiveCoding_Middle_CleanArch.Application.Interfaces;

public interface IBlobStorageService
{
    Task<T?> LoadAsync<T>(string containerName, string blobName, CancellationToken cancellationToken);
    Task SaveAsync<T>(string containerName, string blobName, T data, CancellationToken cancellationToken);
}