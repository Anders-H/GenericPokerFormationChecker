using System.Collections.Generic;
using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public class ActionList : List<Action>
{
    public bool IsAllowed(Action action) =>
        this.Any(x => x == action);

}