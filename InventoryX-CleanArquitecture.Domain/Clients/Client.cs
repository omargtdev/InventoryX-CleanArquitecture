using InventoryX_CleanArquitecture.Domain.Primitives;
using InventoryX_CleanArquitecture.Domain.ValueObjects;

namespace InventoryX_CleanArquitecture.Domain.Clients;
public sealed class Client : AggregateRoot
{
    public Client(ClientId id, string name, string lastName, ClientDocument document, Email email, PhoneNumber phoneNumber, string address)
    {
        Id = id;
        Name = name;
        LastName = lastName;
        Document = document;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
    }

    public Client() { }

    public ClientId Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string FullName { get => $"{Name} {LastName}"; }
    public ClientDocument Document { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public string Address { get; private set; } = string.Empty;
}
