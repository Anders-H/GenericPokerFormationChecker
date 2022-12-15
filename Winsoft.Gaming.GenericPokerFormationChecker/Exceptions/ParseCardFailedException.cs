using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;

public class ParseCardFailedException : SystemException
{
    internal ParseCardFailedException(string message) : base(message)
    {
    }
}