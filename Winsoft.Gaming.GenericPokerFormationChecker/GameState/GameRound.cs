namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState;

public class GameRound
{
    public PlayerTurn RoundStarter { get; private set; }
    public PlayerTurn WaitingForPlayer { get; private set; }
    public PlayedActionList RoundHistory { get; private set; }
    public bool PotStays { get; private set; }

    public GameRound()
    {
        RoundStarter = PlayerTurn.Player1;
        WaitingForPlayer = RoundStarter;
        RoundHistory = new PlayedActionList();
        PotStays = false;
    }

    public void Reset()
    {
        RoundStarter = PlayerTurn.Player1;
        WaitingForPlayer = RoundStarter;
        RoundHistory = new PlayedActionList();
        PotStays = false;
    }

    public void BeginNewRound()
    {
        RoundStarter = RoundStarter == PlayerTurn.Player1 ? PlayerTurn.Player2 : PlayerTurn.Player1;
        WaitingForPlayer = RoundStarter;
        RoundHistory.Clear();
        PotStays = false;
    }

    public ActionList GetAllowedActions()
    {
        var result = new ActionList();

        if (RoundHistory.IsFirstMove || RoundHistory.BothPlayerHasChangedCards || RoundHistory.LastMoveIsPass)
        {
            result.Add(Action.Pass);
            result.Add(Action.Bet);
            result.Add(Action.Drop);
        }
        else if (RoundHistory.OnePlayerHasChangedCards)
        {
            result.Add(Action.ChangeCards);
        }
        else if (RoundHistory.LastTwoMovesArePasses)
        {
            PotStays = !RoundHistory.BothPlayerHasChangedCards;
        }
        else
        {
            result.Add(Action.Raise);
            result.Add(Action.Call);
            result.Add(Action.Drop);
        }

        return result;
    }
}