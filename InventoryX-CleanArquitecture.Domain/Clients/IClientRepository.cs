using InventoryX_CleanArquitecture.Domain.ValueObjects;

namespace InventoryX_CleanArquitecture.Domain.Clients;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(ClientId id);
    Task Add(Client client);
    Task<Client?> GetByEmailAsync(Email email);
}
