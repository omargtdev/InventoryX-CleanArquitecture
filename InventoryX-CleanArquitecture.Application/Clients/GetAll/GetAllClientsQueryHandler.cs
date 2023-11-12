using ErrorOr;
using InventoryX_CleanArquitecture.Application.Clients.Models;
using InventoryX_CleanArquitecture.Domain.Clients;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Clients.GetAll;

public sealed class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, ErrorOr<IReadOnlyList<ClientModel>>>
{
    private readonly IClientRepository _clientRepository;

    public GetAllClientsQueryHandler(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
    }

    public async Task<ErrorOr<IReadOnlyList<ClientModel>>>Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await _clientRepository.GetAllAsync();

        return clients.Select(c => new ClientModel(
            c.Id.Value,
            c.Name,
            c.LastName,
            c.FullName,
            c.PhoneNumber.Value,
            c.Email.Value,
            new ClientDocumentModel(
                (int)c.Document.DocumentType,
                c.Document.DocumentNumber),
            c.Address
        )).ToList();
    }
}
