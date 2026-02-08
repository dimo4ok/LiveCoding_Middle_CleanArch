using LiveCoding_Middle_CleanArch.API.Endpoints.Routes;
using LiveCoding_Middle_CleanArch.Application.Common.Models;
using LiveCoding_Middle_CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LiveCoding_Middle_CleanArch.API.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet(ProductRoutes.Fetch, async 
            (
            [FromServices]IProductService service,
            CancellationToken cancellationToken = default
            ) => 
        {
            var response = await service.FetchProductAsync(cancellationToken);
            return Results.Json(response, statusCode: response.StatusCode); 
        });

        app.MapGet(ProductRoutes.GetAll, async
            (
            [FromServices]IProductService service,
            CancellationToken cancellationToken = default
            ) =>
        {
            var response = await service.GetAllProductsAsync(cancellationToken);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        app.MapGet(ProductRoutes.GetById, async
          (
          [FromServices] IProductService service,
          int id,
          CancellationToken cancellationToken = default
          ) =>
        {
            var response = await service.GetProductByIdAsync(id, cancellationToken);
            return Results.Json(response, statusCode: response.StatusCode);
        });

        app.MapPut(ProductRoutes.Update, async
        (
        [FromServices] IProductService service,
        int id, 
        UpdateProductModel model,
        CancellationToken cancellationToken = default
        ) =>
        {
            var response = await service.UpdateProductAsync(id, model, cancellationToken);
            return Results.Json(response, statusCode: response.StatusCode);
        });


        app.MapDelete(ProductRoutes.Delete, async
        (
        [FromServices] IProductService service,
        int id,
        CancellationToken cancellationToken = default
        ) =>
        {
            var response = await service.DeleteProductAsync(id, cancellationToken);
            return Results.Json(response, statusCode: response.StatusCode);
        });
    }
}
