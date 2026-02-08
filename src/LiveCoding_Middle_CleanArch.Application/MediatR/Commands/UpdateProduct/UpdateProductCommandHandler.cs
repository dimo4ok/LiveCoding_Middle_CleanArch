using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IBlobStorageService blobStorageService) : IRequestHandler<UpdateProductCommand, Result>
{

    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var updateToData = response.Products.FirstOrDefault(x => x.Id == request.Id);
        if (updateToData is null)
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));

        var updateIndex = Array.FindIndex(response.Products, x => x.Id == request.Id);
        response.Products[updateIndex] = request.Model.ToEntity(request.Id);

        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result.Success();
    }
}
