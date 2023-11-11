using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.Exceptions;
using InventoryX_CleanArquitecture.Domain.Primitives;
using InventoryX_CleanArquitecture.Domain.ValueObjects;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Clients.Create;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Unit>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Unit> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        if (Email.Create(command.Email) is not Email email)
            throw new EntityRuleException(nameof(email));
        
        if(PhoneNumber.Create(command.PhoneNumber) is not PhoneNumber phoneNumber)
            throw new EntityRuleException(nameof(phoneNumber));

        if (ClientDocument.Create(command.DocumentType, command.DocumentNumber) is not ClientDocument clientDocument)
            throw new EntityRuleException(nameof(clientDocument));

        var client = new Client(
            new ClientId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            clientDocument,
            email,
            phoneNumber,
            command.Address
        );

        await _clientRepository.Add(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
