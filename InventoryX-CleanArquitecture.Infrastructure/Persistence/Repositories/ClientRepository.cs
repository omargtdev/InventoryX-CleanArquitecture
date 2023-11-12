using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InventoryX_CleanArquitecture.Infrastructure.Persistence.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task Add(Client client) => await _context.Clients.AddAsync(client);

    public async Task<ICollection<Client>> GetAllAsync() =>
        await _context.Clients.Where(c => !c.IsDeleted).ToListAsync();

    public async Task<bool> IsEmailUniqueAsync(Email email) =>
        await _context.Clients.AnyAsync(c => !c.IsDeleted && c.Email != email);

    public async Task<Client?> GetByIdAsync(ClientId id) => await _context.Clients.SingleOrDefaultAsync(c => c.Id == id);
}
