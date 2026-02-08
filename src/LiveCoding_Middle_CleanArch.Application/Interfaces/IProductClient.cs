namespace LiveCoding_Middle_CleanArch.Application.Interfaces;

public interface IProductClient
{
    Task<T?> FetchAsync<T>(string url, CancellationToken cancellationToken = default);
}