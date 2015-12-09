using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Exceptions
{
    public class ParseCardFailed : Exception
    {
        internal ParseCardFailed(string message) : base(message)
        {
        }
    }

    public class ParseSuitFailed : ParseCardFailed
    {
        internal ParseSuitFailed(string message) : base(message)
        {
        }
    }

    public class ParseHandFailed : Exception
    {
        internal ParseHandFailed(string message) : base(message)
        {
        }
    }
}
