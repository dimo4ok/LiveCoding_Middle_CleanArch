using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.MediatR.Commands.DeleteProduct;
using LiveCoding_Middle_CleanArch.Application.MediatR.Commands.FetchProducts;
using LiveCoding_Middle_CleanArch.Application.MediatR.Commands.UpdateProduct;
using LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetAllProducts;
using LiveCoding_Middle_CleanArch.Application.MediatR.Queries.GetProductById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding_Middle_CleanArch.API.Controllers;

[ApiController]
[Route("api1/[controller]")]
public class ProductController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet("fetch")]
    public async Task<IActionResult> FetchProducts(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new FetchProductsCommand(), cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody]UpdateProductModel model, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new UpdateProductCommand(id, model),cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }
}
