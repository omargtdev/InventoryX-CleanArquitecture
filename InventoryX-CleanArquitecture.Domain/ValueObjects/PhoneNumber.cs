using System.Text.RegularExpressions;

namespace InventoryX_CleanArquitecture.Domain.ValueObjects;

public partial record PhoneNumber
{
    private const int DefaultLength = 9;
    private const string Pattern = @"^\d{9}$";

    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber? Create(string value)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        if (!Regex.Match(value, Pattern).Success)
            return null;

        if(value.Length != DefaultLength)
            return null;

        return new PhoneNumber(value);
    }

    public string Value { get; init; }

}