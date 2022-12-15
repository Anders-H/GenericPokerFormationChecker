using System.Collections.Generic;
using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public class PlayedActionList : List<PlayedAction>
{
    public bool IsFirstMove =>
        Count <= 0;

    public bool OnePlayerHasChangedCards =>
        this.Count(x => x.Action == Action.ChangeCards) == 1;

    public bool BothPlayerHasChangedCards =>
        this.Count(x => x.Action == Action.ChangeCards) == 2;

    public bool LastTwoMovesArePasses =>
        Count >= 2 && this[Count - 1].Action == Action.Pass && this[Count - 2].Action == Action.Pass;

    public bool LastMoveIsPass =>
        !LastTwoMovesArePasses && Count >= 1 && this[Count - 1].Action == Action.Pass;
}