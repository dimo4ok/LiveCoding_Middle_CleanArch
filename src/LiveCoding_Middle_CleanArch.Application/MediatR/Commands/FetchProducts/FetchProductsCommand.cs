using LiveCoding_Middle_CleanArch.Application.Common.Response;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.FetchProducts;

public record FetchProductsCommand() : IRequest<Result<int>>;
