namespace InventoryX_CleanArquitecture.Domain.Exceptions;

public class BaseException : Exception
{
    public BaseException(string message) : base(message) { }

    public string Name { get => GetType().Name; }
}
