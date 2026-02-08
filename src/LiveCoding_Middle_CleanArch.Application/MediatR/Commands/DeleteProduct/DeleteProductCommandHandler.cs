using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IBlobStorageService blobStorageService
    ) : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;

    private const string containerName = "products";
    private const string blobName = "products.json";

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var response = await _blobStorageService.LoadAsync<ProductList>(containerName, blobName, cancellationToken);
        if (response is null)
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));

        var updateToData = response.Products.FirstOrDefault(x => x.Id == request.Id);
        if (updateToData is null)
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));

        response.Products = response.Products.Where(x => x.Id != request.Id).ToArray();
        await _blobStorageService.SaveAsync(containerName, blobName, response, cancellationToken);

        return Result.Success(StatusCodes.Status204NoContent);
    }
}
