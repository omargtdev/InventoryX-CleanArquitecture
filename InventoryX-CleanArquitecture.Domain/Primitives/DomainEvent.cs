using MediatR;

namespace InventoryX_CleanArquitecture.Domain.Primitives;

public record DomainEvent(Guid Id) : INotification;