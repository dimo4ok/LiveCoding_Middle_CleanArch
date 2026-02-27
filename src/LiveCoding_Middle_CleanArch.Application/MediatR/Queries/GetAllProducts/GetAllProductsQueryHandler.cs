using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Extensions;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetAllProducts;

public class GetAllProductsQueryHandler(
    IBlobStorageService blobStorageService,
    IOptions<StorageSettings> options,
    ILogger<GetAllProductsQueryHandler> logger
    ) : IRequestHandler<GetAllProductsQuery, Result<ProductListModel>>
{
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<GetAllProductsQueryHandler> _logger = logger;

    private readonly StorageSettings _settings = options.Value;

    public async Task<Result<ProductListModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetAllProductsAsync: Starting request.");

        var response = await _blobStorageService.LoadAsync<ProductList>(_settings.ContainerName, _settings.BlobName, cancellationToken);
        if (response is null)
        {
            _logger.LogWarning("GetAllProductsAsync: products.json not found in storage.");
            return Result<ProductListModel>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        _logger.LogInformation("GetAllProductsAsync: Completed successfully.");
        return Result<ProductListModel>.Success(response.ToModel());
    }
}
