using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LiveCoding_Middle_CleanArch.Application.Services;

public class ProductService(IProductClient apiPorductService, IBlobStorageService blobStorageService) : IProductService
{
    private readonly IProductClient _apiPorductService = apiPorductService;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";
    private const string BaseAddress = "https://dummyjson.com/products";

    public async Task<Result<int>> FetchProductAsync(CancellationToken cancellationToken = default)
    {
        var response = await _apiPorductService.FetchAsync<ProductList>(BaseAddress, cancellationToken);
        if (response is null)
            return Result<int>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result<int>.Success(response.Products.Length);
    }

    public async Task<Result<ProductListModel>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result<ProductListModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        return Result<ProductListModel>.Success(response.ToModel());
    }

    public async Task<Result<ProductModel>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var data = response.Products.FirstOrDefault(x => x.Id == id);
        if(data is null)
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(Product)));

        return Result<ProductModel>.Success(data.ToModel());
    }

    public async Task<Result> UpdateProductAsync(int id, UpdateProductModel model, CancellationToken cancellationToken = default)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var updateToData = response.Products.FirstOrDefault(x => x.Id == id);
        if (updateToData is null)
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));

        var updateIndex = Array.FindIndex(response.Products, x => x.Id == id);
        response.Products[updateIndex] = model.ToEntity(id);

        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteProductAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var updateToData = response.Products.FirstOrDefault(x => x.Id == id);
        if (updateToData is null)
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));

        response.Products = response.Products.Where(x => x.Id != id).ToArray();
        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}

