using LiveCoding_Middle_CleanArch.Application.Common.Errors;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using LiveCoding_Middle_CleanArch.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.FetchProducts;

public class FetchProductsCommandHandler(
    IProductClient apiPorductService,
    IBlobStorageService blobStorageService,
    IOptions<StorageSettings> options,
    ILogger<FetchProductsCommandHandler> logger
    ) : IRequestHandler<FetchProductsCommand, Result<int>>
{
    private readonly IProductClient _apiPorductService = apiPorductService;
    private readonly IBlobStorageService _blobStorageService = blobStorageService;
    private readonly ILogger<FetchProductsCommandHandler> _logger = logger;

    private readonly StorageSettings _settings = options.Value;

    public async Task<Result<int>> Handle(FetchProductsCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("FetchProductAsync: Starting fetch from {BaseAddress}", _settings.BaseAddress);

        var response = await _apiPorductService.FetchAsync<ProductList>(_settings.BaseAddress, cancellationToken);
        if (response is null)
        {
            _logger.LogError("FetchProductAsync: Failed to fetch data from API.");
            return Result<int>.Fail(CommonErrors.NotFound(nameof(ProductList)));
        }

        await _blobStorageService.SaveAsync(_settings.ContainerName, _settings.BlobName, response, cancellationToken);

        _logger.LogInformation("FetchProductAsync: Completed. Saved {Count} products.", response.Products.Length);
        return Result<int>.Success(response.Products.Length);
    }
}