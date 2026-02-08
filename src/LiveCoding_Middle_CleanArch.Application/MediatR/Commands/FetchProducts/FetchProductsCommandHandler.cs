using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.FetchProducts;

public class FetchProductsCommandHandler(
    IProductClient apiPorductService, 
    IBlobStorageService blobStorageService
    ) : IRequestHandler<FetchProductsCommand, Result<int>>
{
    private readonly IProductClient _apiPorductService = apiPorductService;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";
    private const string BaseAddress = "https://dummyjson.com/products";

    public async Task<Result<int>> Handle(FetchProductsCommand request, CancellationToken cancellationToken)
    {
        var response = await _apiPorductService.FetchAsync<ProductList>(BaseAddress, cancellationToken);
        if (response is null)
            return Result<int>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result<int>.Success(response.Products.Length);
    }
}