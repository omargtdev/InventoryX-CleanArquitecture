using System.Text.RegularExpressions;

namespace InventoryX_CleanArquitecture.Domain.Clients;

public partial record ClientDocument
{
    private const int DNILength = 8;
    private const int RUCLength = 11;
    private const int ImmigrationCardLength = 15;
    private const string PatternRule = @"^\d+$";

    private ClientDocument(DocumentType type, string number)
    {
        DocumentType = type;
        DocumentNumber = number;
    }

    public ClientDocument() { }

    public static ClientDocument? Create(int type, string number)
    {
        if (string.IsNullOrEmpty(number))
            return null;

        if(!Regex.Match(number, PatternRule).Success)
            return null;

        if (!Enum.IsDefined(typeof(DocumentType), type))
            return null;

        bool isValid = false;
        DocumentType documentType = (DocumentType)type;
        switch (documentType)
        {
            case DocumentType.DNI:
                if (number.Length == DNILength)
                    isValid = true;
                break;
            case DocumentType.RUC:
                if(number.Length == RUCLength)
                    isValid = true;
                break;
            case DocumentType.ImmigrationCard:
                if(number.Length == ImmigrationCardLength)
                    isValid = true;
                break;
        }

        if (!isValid)
            return null;

        return new ClientDocument(documentType, number);
    }

    public DocumentType DocumentType { get; init; }
    public string DocumentNumber { get; init; }

}

public enum DocumentType
{
    DNI = 1,
    RUC = 2,
    ImmigrationCard = 3
}
