using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.UpdateProduct;

public class UpdateProductCommandHandler(
    IBlobStorageService blobStorageService,
    IOptions<StorageSettings> options,
    ILogger<UpdateProductCommandHandler> logger
    ) : IRequestHandler<UpdateProductCommand, Result>
{

    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<UpdateProductCommandHandler> _logger = logger;

    private readonly StorageSettings _settings = options.Value;

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("UpdateProductAsync: Starting for Id {Id}", request.Id);

        var response = await _blobStorageService.LoadAsync<ProductList>(_settings.ContainerName, _settings.BlobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("UpdateProductAsync: Storage file missing.");
            return Result.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        var updateIndex = Array.FindIndex(response.Products, x => x.Id == request.Id);
        if (updateIndex == -1)
        {
            _logger.LogWarning("UpdateProductAsync: Id {Id} not found.", request.Id);
            return Result.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        response.Products[updateIndex] = request.Model.ToEntity(request.Id);
        await _blobStorageService.SaveAsync(_settings.ContainerName, _settings.BlobName, response, cancellationToken);

        _logger.LogInformation("UpdateProductAsync: Completed for Id {Id}", request.Id);
        return Result.Success();
    }
}
