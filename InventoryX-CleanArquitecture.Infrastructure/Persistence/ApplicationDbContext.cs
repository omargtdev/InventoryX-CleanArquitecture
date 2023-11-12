using InventoryX_CleanArquitecture.Application.Data;
using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InventoryX_CleanArquitecture.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
    {
        _publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
    }

    public DbSet<Client> Clients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Custom configurations (Not apply to domain entities)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach(var entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(e => e.UpdatedOn)
                  .CurrentValue = DateTime.UtcNow;
            }

            if(entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(e => e.UpdatedOn)
                  .CurrentValue = DateTime.UtcNow;
            }
              
            if(entityEntry.State == EntityState.Deleted)
            {
                entityEntry.State = EntityState.Modified;
                entityEntry.Property(e => e.DeletedOn)
                    .CurrentValue= DateTime.UtcNow;
                entityEntry.Property(e => e.IsDeleted)
                    .CurrentValue = true;
            }
        }
        

        var result = await base.SaveChangesAsync(cancellationToken);

        // Publishing the events
        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .SelectMany(e => e.DomainEvents);

        foreach (var domainEvent in domainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);

        return result;
    }
}
