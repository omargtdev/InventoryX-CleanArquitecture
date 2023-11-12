using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace InventoryX_CleanArquitecture.API.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = "Server Error",
                Title = "Server Error",
                Detail = "An internal server error has ocurred."
            };

            string json = JsonSerializer.Serialize(problemDetails);
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsync(json);
        }
    }
}
