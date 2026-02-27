using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetProductById;

public class GetProductByIdQueryHandler(
    IBlobStorageService blobStorageService,
    IOptions<StorageSettings> options,
    ILogger<GetProductByIdQueryHandler> logger
    ) : IRequestHandler<GetProductByIdQuery, Result<ProductModel>>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<GetProductByIdQueryHandler> _logger = logger;

    private readonly StorageSettings _settings = options.Value;

    public async Task<Result<ProductModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductByIdAsync: Starting for Id {Id}", request.Id);

        var response = await _blobStorageService.LoadAsync<ProductList>(_settings.ContainerName, _settings.BlobName, cancellationToken);
        if (response is null)
        {
            _logger.LogError("GetProductByIdAsync: Storage file missing.");
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        var data = response.Products.FirstOrDefault(x => x.Id == request.Id);
        if (data is null)
        {
            _logger.LogWarning("GetProductByIdAsync: Product Id {Id} not found.", request.Id);
            return Result<ProductModel>.Fail(CommonErrors.NotFound(nameof(Product)));
        }

        _logger.LogInformation("GetProductByIdAsync: Completed for Id {Id}", request.Id);
        return Result<ProductModel>.Success(data.ToModel());
    }
}
