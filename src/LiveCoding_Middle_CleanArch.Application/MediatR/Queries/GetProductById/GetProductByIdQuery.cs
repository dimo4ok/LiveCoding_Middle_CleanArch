using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using MediatR;


namespace LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetProductById;

public record GetProductByIdQuery(int Id) : IRequest<Result<ProductModel>>;
