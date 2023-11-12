using ErrorOr;
using InventoryX_CleanArquitecture.Application.Clients.Models;
using MediatR;

namespace InventoryX_CleanArquitecture.Application.Clients.GetAll;

public record GetAllClientsQuery : IRequest<ErrorOr<IReadOnlyList<ClientModel>>>;
