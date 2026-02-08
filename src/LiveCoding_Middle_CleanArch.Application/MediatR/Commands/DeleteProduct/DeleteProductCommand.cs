using LiveCoding_Middle_CleanArch.Application.Common.Response;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<Result>;
