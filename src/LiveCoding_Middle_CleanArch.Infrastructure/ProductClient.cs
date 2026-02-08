using LiveCoding_Middle_CleanArch.Application.Interfaces;
using System.Net.Http.Json;

namespace LiveCoding_Middle_CleanArch.Infrastructure;

public class ProductClient(IHttpClientFactory httpClientFactory) : IProductClient
{
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient();
    //private readonly HttpClient _httpClient = httpClientFactory.CreateClient("ProductApi");

    public async Task<T?> FetchAsync<T>(string url, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<T>(url, cancellationToken);
        //return await _httpClient.GetFromJsonAsync<T>("products", cancellationToken);
    }
}
