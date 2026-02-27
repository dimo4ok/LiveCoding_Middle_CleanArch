using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LiveCoding_Middle_CleanArch.Application.Services;

public class ProductService(
    IProductClient apiPorductService,
    IBlobStorageService blobStorageService,
    ILogger<ProductService> logger) : IProductService
{
    private readonly IProductClient _apiPorductService = apiPorductService;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<ProductService> _logger = logger;

    private const string containerName = "products";
    private const string blobName = "products.json";
    private const string BaseAddress = "https://dummyjson.com/products";

    public async Task<Result<int>> FetchProductAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("FetchProductAsync: Starting fetch from {BaseAddress}", BaseAddress);

        var response = await _apiPorductService.FetchAsync<ProductList>(BaseAddress, cancellationToken);
        if (response is null)
        {
            _logger.LogError("FetchProductAsync: Failed to fetch data from API.");
            return Result<int>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        _logger.LogInformation("FetchProductAsync: Completed. Saved {Count} products.", response.Products.Length);
        return Result<int>.Success(response.Products.Length);
    }

    public async Task<Result<ProductListModel>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GetAllProductsAsync: Starting request.");

        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
        {
            _logger.LogWarning("GetAllProductsAsync: products.json not found in storage.");
            return Result<ProductListModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        _logger.LogInformation("GetAllProductsAsync: Completed successfully.");
        return Result<ProductListModel>.Success(response.ToModel());
    }

    public async Task<Result<ProductModel>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("GetProductByIdAsync: Starting for Id {Id}", id);

        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("GetProductByIdAsync: Storage file missing.");
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        var data = response.Products.FirstOrDefault(x => x.Id == id);
        if (data is null)
        {
            _logger.LogWarning("GetProductByIdAsync: Product Id {Id} not found.", id);
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        _logger.LogInformation("GetProductByIdAsync: Completed for Id {Id}", id);
        return Result<ProductModel>.Success(data.ToModel());
    }

    public async Task<Result> UpdateProductAsync(int id, UpdateProductModel model, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("UpdateProductAsync: Starting for Id {Id}", id);

        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("UpdateProductAsync: Storage file missing.");
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        var updateIndex = Array.FindIndex(response.Products, x => x.Id == id);
        if (updateIndex == -1)
        {
            _logger.LogWarning("UpdateProductAsync: Id {Id} not found.", id);
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        response.Products[updateIndex] = model.ToEntity(id);
        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        _logger.LogInformation("UpdateProductAsync: Completed for Id {Id}", id);
        return Result.Success();
    }

    public async Task<Result> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("DeleteProductAsync: Starting for Id {Id}", id);

        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("DeleteProductAsync: Storage file missing.");
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        var productExists = response.Products.Any(x => x.Id == id);
        if (!productExists)
        {
            _logger.LogWarning("DeleteProductAsync: Id {Id} not found.", id);
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        response.Products = response.Products.Where(x => x.Id != id).ToArray();
        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        _logger.LogInformation("DeleteProductAsync: Completed for Id {Id}", id);
        return Result.Success(StatusCodes.Status204NoContent);
    }
}