using InventoryX_CleanArquitecture.Domain.Clients;
using InventoryX_CleanArquitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryX_CleanArquitecture.Infrastructure.Persistence.Configuration;

public class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    public void Configure(EntityTypeBuilder<Client> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id).HasConversion(
            clientId => clientId.Value,
            value => new ClientId(value));

        builder.Property(c => c.Name)
            .HasMaxLength(100);

        builder.Property(c => c.LastName)
            .HasMaxLength(150);

        builder.Property(c => c.Email).HasConversion(
            email => email.Value,
            value => Email.Create(value)!);

        builder.HasIndex(c => c.Email).IsUnique();

        builder.Property(c => c.PhoneNumber).HasConversion(
            phoneNumber => phoneNumber.Value,
            value => PhoneNumber.Create(value)!)
            .HasMaxLength(9)
            .IsRequired();

        builder.Property(c => c.Address)
            .HasMaxLength(250);

        builder.ToTable(nameof(Client));
    }
}
