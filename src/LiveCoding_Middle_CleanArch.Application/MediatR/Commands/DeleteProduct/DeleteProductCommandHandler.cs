using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IBlobStorageService blobStorageService,
    IOptions<StorageSettings> options,
    ILogger<DeleteProductCommandHandler> logger
    ) : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<DeleteProductCommandHandler> _logger = logger;

    private readonly StorageSettings _settings = options.Value;

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("DeleteProductAsync: Starting for Id {Id}", request.Id);

        var response = await _blobStorageService.LoadAsync<ProductList>(_settings.ContainerName, _settings.BlobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("DeleteProductAsync: Storage file missing.");
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));

        }

        var updateToData = response.Products.FirstOrDefault(x => x.Id == request.Id);
        if (updateToData is null)
        {
            _logger.LogWarning("DeleteProductAsync: Id {Id} not found.", request.Id);

        return Result.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        response.Products = response.Products.Where(x => x.Id != request.Id).ToArray();
        await _blobStorageService.SaveAsync(_settings.ContainerName, _settings.BlobName, response, cancellationToken);

        _logger.LogInformation("DeleteProductAsync: Completed for Id {Id}", request.Id);
        return Result.Success(StatusCodes.Status204NoContent);
    }
}
