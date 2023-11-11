namespace InventoryX_CleanArquitecture.Domain.Clients;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(ClientId id);
    Task Add(Client client);
}
