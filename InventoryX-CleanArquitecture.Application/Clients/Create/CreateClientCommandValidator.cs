using FluentValidation;
using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.ValueObjects;

namespace InventoryX_CleanArquitecture.Application.Clients.Create;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public CreateClientCommandValidator(IClientRepository clientRepository) 
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));

        RuleFor(c => c.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(c => c.LastName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(c => c.Address)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(c => c.PhoneNumber)
            .Must(phoneNumber =>
                 PhoneNumber.Create(phoneNumber) is not null);

        RuleFor(c => c.Email)
            .Must(email =>
                Email.Create(email) is not null)
            .WithMessage("The email is invalid");

        RuleFor(c => c.Email)
            .MustAsync(async (email, cancellation) =>
            {
                return await _clientRepository.GetByEmailAsync(Email.Create(email)!) is null;
            })
            .WithMessage("Email already exists for the client!");

        RuleFor(c => c)
            .Must(client =>
                ClientDocument.Create(client.DocumentType, client.DocumentNumber) is not null)
            .OverridePropertyName("Document Client")
            .WithMessage("The document is invalid.");
    }

}
