using ErrorOr;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Common.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> 
    where TResponse : IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if(!_validators.Any())
            return await next();

        var validations = new List<ValidationResult>();
        foreach (var validation in _validators)
        {
            var result = await validation.ValidateAsync(request, cancellationToken);
            validations.Add(result);
        }
        
        var errors = validations
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .ToList()
            .ConvertAll(v => Error.Validation(
                v.PropertyName,
                v.ErrorMessage));

        if (!errors.Any())
            await next();

        return (dynamic)errors;
    }
}
