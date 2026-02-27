using LiveCoding_Middle_CleanArch.Application.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace LiveCoding_Middle_CleanArch.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAzureClients(client =>
        client.AddBlobServiceClient(configuration.GetSection("ConnectionStrings:AzureStorage")));

        services.AddHttpClient();
        //services.AddHttpClient("ProductApi", client =>
        //{
        //    client.BaseAddress = new Uri("https://dummyjson.com/");
        //});

        services.AddScoped<IProductClient, ProductClient>();
        services.AddScoped<IBlobStorageService, BlobStorageService>();

        return services;
    }
}