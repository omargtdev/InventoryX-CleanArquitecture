using System.Net.Mail;

namespace InventoryX_CleanArquitecture.Domain.ValueObjects;

public partial record Email
{
    private Email(string value) => Value = value;

    public static Email? Create(string email)
    {
        bool emailWasValid = MailAddress.TryCreate(email, out _);
        if (!emailWasValid)
            return null;

        return new Email(email);
    }

    public string Value { get; init; }

}
