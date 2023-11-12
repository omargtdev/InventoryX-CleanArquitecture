using ErrorOr;
using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.Primitives;
using InventoryX_CleanArquitecture.Domain.ValueObjects;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Clients.Create;

public sealed class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ErrorOr<ClientId>>
{
    private readonly IClientRepository _clientRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ErrorOr<ClientId>> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        var client = new Client(
            new ClientId(Guid.NewGuid()),
            command.Name,
            command.LastName,
            ClientDocument.Create(command.DocumentType, command.DocumentNumber)!,
            Email.Create(command.Email)!,
            PhoneNumber.Create(command.PhoneNumber)!,
            command.Address
        );
        client.CreatedBy = "System";

        await _clientRepository.Add(client);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
