namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public class PlayedAction
{
    public PlayerTurn Player { get; }
    public Action Action { get; }
    public int Parameter { get; }

    public PlayedAction(PlayerTurn player, Action action, int parameter)
    {
        Player = player;
        Action = action;
        Parameter = parameter;
    }
}