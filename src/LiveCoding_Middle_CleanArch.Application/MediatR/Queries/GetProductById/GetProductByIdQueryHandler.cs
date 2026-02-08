using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IBlobStorageService blobStorageService
    ) : IRequestHandler<GetProductByIdQuery, Result<ProductModel>>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";

    public async Task<Result<ProductModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var data = response.Products.FirstOrDefault(x => x.Id == request.Id);
        if (data is null)
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(Product)));

        return Result<ProductModel>.Success(data.ToModel());
    }
}
