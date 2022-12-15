namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;

public class ParseSuitFailedException : ParseCardFailedException
{
    internal ParseSuitFailedException(string message) : base(message)
    {
    }
}