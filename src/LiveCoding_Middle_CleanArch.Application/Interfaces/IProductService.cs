using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using LiveCoding_Middle_CleanArch.Domain.Entities;

namespace LiveCoding_Middle_CleanArch.Application.Interfaces;

public interface IProductService
{
    Task<Result> DeleteProductAsync(int id, CancellationToken cancellationToken = default);
    Task<Result<int>> FetchProductAsync(CancellationToken cancellationToken = default);
    Task<Result<ProductListModel>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<Result<ProductModel>> GetProductByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<Result> UpdateProductAsync(int id, UpdateProductModel model, CancellationToken cancellationToken = default);
}