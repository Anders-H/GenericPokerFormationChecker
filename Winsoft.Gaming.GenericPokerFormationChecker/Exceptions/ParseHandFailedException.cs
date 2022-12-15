using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;

public class ParseHandFailedException : SystemException
{
    internal ParseHandFailedException(string message) : base(message)
    {
    }
}