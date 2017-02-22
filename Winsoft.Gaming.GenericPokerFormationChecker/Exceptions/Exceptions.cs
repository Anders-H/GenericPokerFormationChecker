using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions
{
    public class ParseCardFailedException : SystemException
    {
        internal ParseCardFailedException(string message) : base(message)
        {
        }
    }

    public class ParseSuitFailedException : ParseCardFailedException
    {
        internal ParseSuitFailedException(string message) : base(message)
        {
        }
    }

    public class ParseHandFailedException : SystemException
    {
        internal ParseHandFailedException(string message) : base(message)
        {
        }
    }

    public class DuplicateCardException : SystemException
    {
        public DuplicateCardException(string message) : base(message)
        {
        }
    }
}
