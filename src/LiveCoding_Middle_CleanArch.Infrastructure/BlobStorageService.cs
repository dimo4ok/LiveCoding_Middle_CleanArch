using Azure.Storage.Blobs;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using System.Text.Json;

namespace LiveCoding_Middle_CleanArch.Infrastructure;

public class BlobStorageService(BlobServiceClient blobServiceClient) : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient = blobServiceClient;

    public async Task SaveAsync<T>(string containerName, string blobName, T data, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);

        var blobClient = containerClient.GetBlobClient(blobName);

        var content = JsonSerializer.Serialize(data);

        await blobClient.UploadAsync(new BinaryData(content), true, cancellationToken);
    }

    public async Task<T?> LoadAsync<T>(string containerName, string blobName, CancellationToken cancellationToken)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        if (containerClient is null)
            return default;

        var blobClient = containerClient.GetBlobClient(blobName);
        if (!await blobClient.ExistsAsync(cancellationToken))
            return default;

        var data = await blobClient.DownloadContentAsync(cancellationToken);

        if(data.Value.Content.Length == 0)
            return default;

        return JsonSerializer.Deserialize<T>(data.Value.Content.ToString());
    }
}
