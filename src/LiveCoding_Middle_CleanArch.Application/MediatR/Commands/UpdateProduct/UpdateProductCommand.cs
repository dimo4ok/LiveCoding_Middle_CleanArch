using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Common.Response;
using MediatR;

namespace LiveCoding_Middle_CleanArch.Application.MediatR.Commands.UpdateProduct;

public record UpdateProductCommand(int Id, UpdateProductModel Model) : IRequest<Result>;
