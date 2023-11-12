namespace InventoryX_CleanArquitecture.Application.Clients.Models;

public record ClientModel(
    Guid Id,
    string Name,
    string LastName,
    string FullName,
    string PhoneNumber,
    string Email,
    ClientDocumentModel Document,
    string Address);

public record ClientDocumentModel(
    int DocumentType,
    string DocumentNumber);
