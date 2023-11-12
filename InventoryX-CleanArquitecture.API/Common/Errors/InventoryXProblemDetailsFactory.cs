using ErrorOr;
using InventoryX_CleanArquitecture.API.Common.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Diagnostics;

namespace InventoryX_CleanArquitecture.API.Common.Errors;

public class InventoryXProblemDetailsFactory : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options;

    public InventoryXProblemDetailsFactory(ApiBehaviorOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext,
        int? statusCode = null,
        string? title = null, 
        string? type = null, 
        string? detail = null,
        string? instance = null)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode ?? StatusCodes.Status500InternalServerError,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode);

        return problemDetails;
    }

    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int? statusCode)
    {
        if(_options.ClientErrorMapping.TryGetValue(
            statusCode ?? StatusCodes.Status500InternalServerError, 
            out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        if(traceId is not null)
            problemDetails.Extensions["traceId"] = traceId;

        var errors = httpContext?.Items[HttpContextItemKeys.Errors] as List<Error>;
        if (errors is not null)
            problemDetails.Extensions.Add("errorCodes", errors.Select(e => e.Code));
    }

    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        if(modelStateDictionary is null)
            throw new ArgumentNullException(nameof(modelStateDictionary));

        var validationProblemDetails = new ValidationProblemDetails
        {
            Status = statusCode ?? StatusCodes.Status400BadRequest,
            Type = type,
            Detail = detail,
            Instance = instance
        };

        if (title is not null)
            validationProblemDetails.Title = title;

        ApplyProblemDetailsDefaults(httpContext, validationProblemDetails, statusCode);
            
        return validationProblemDetails;
    }
}
