using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IBlobStorageService blobStorageService
    ) : IRequestHandler<GetAllProductsQuery, Result<ProductListModel>>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";

    public async Task<Result<ProductListModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result<ProductListModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        return Result<ProductListModel>.Success(response.ToModel());
    }
}
