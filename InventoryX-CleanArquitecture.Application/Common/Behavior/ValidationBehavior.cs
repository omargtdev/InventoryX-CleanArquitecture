using ErrorOr;
using FluentValidation;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;
    
    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(_validator is null)
            return await next();

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
            return await next();

        var errors = validationResult.Errors
            .ConvertAll(v => Error.Validation(
                v.PropertyName,
                v.ErrorMessage));

        return (dynamic)errors;
    }
}
