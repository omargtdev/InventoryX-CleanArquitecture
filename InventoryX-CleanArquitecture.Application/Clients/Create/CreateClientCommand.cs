using ErrorOr;
using InventoryX_CleanArquitecture.Domain.Clients;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Clients.Create;

public record CreateClientCommand(
    string Name,
    string LastName,
    string Email,
    string PhoneNumber,
    int DocumentType,
    string DocumentNumber,
    string Address) : IRequest<ErrorOr<ClientId>>;
