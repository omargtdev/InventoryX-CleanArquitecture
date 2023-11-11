using InventoryX_CleanArquitecture.Domain.Clients;
using Microsoft.EntityFrameworkCore;

namespace InventoryX_CleanArquitecture.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Client> Clients { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
