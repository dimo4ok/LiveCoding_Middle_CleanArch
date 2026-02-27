namespace LiveCoding_Middle_CleanArch.Application.Common.Models;

public class StorageSettings
{
    public string ContainerName { get; set; } = string.Empty;
    public string BlobName { get; set; } = string.Empty;
    public string BaseAddress { get; set; } = string.Empty;
}