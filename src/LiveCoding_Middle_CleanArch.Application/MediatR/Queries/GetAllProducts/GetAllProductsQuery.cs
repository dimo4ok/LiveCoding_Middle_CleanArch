using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<Result<ProductListModel>>;
