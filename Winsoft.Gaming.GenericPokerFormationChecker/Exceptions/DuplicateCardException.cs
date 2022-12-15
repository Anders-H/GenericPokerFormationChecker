using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;

public class DuplicateCardException : SystemException
{
    public DuplicateCardException(string message) : base(message)
    {
    }
}