namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public class PlayedAction
{
    public PlayerTurn Player { get; }
    public Action Action { get; }

    public PlayedAction(PlayerTurn player, Action action)
    {
        Player = player;
        Action = action;
    }
}