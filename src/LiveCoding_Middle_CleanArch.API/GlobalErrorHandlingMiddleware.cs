using LiveCoding_Middle_CleanArch.Application.Common.Response;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace LiveCoding_Middle_CleanArch.API;

public class GlobalErrorHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            var errorId = Guid.NewGuid();

            // Log the error with a unique identifier
            _logger.LogError(ex, $"[{errorId}] Unhandled exception: {ex.Message}");

            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(Result.Fail(ex.Message, httpContext.Response.StatusCode));
        }
    }
}
