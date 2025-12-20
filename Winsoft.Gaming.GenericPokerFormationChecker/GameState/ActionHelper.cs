using System;
using System.Collections.Generic;
using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public static class ActionHelper
{
    public static List<Action> GetAll() =>
        Enum.GetValues<Action>().ToList();
}