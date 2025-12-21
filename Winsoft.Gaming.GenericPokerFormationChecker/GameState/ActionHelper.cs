using System;
using System.Collections.Generic;
using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public static class ActionHelper
{
    public static List<Action> GetAll() =>
        Enum.GetValues<Action>().ToList();

    public static string ToReadableString(this Action action) =>
        action switch
        {
            Action.Bet => "Bet",
            Action.Pass => "Pass",
            Action.Drop => "Drop",
            Action.Call => "Call",
            Action.Raise => "Raise",
            Action.ChangeCards => "Change cards",
            _ => "Unknown action"
        };
}