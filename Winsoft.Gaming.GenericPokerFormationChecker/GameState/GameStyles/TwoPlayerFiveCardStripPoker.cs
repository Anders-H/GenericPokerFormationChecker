using System.Text;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;

public class TwoPlayerFiveCardStripPoker
{
    public TwoPlayerFiveCardStripPokerPlayer Player1 { get; }
    public TwoPlayerFiveCardStripPokerPlayer Player2 { get; }
    public GameRound? GameRound { get; private set; }
    public PlayedActionList RoundHistory { get; private set; }

    public TwoPlayerFiveCardStripPoker(TwoPlayerFiveCardStripPokerPlayer humanPlayer, TwoPlayerFiveCardStripPokerPlayer computerPlayer)
    {
        Player1 = humanPlayer;
        Player2 = computerPlayer;
    }

    public GameRound NextRound()
    {
        RoundHistory = [];

        if (GameRound == null)
        {
            GameRound = new GameRound();
            return GameRound;
        }

        GameRound.BeginNewRound();
        return GameRound;
    }

    public override string ToString()
    {
        if (GameRound == null)
            return "Game is not started.";

        var s = new StringBuilder();
        s.AppendLine($"Round stated by {GameRound.RoundStarter} (player {GetPlayerNumber(GameRound.WaitingForPlayer)}).");
        s.AppendLine($"Current player is {GameRound.WaitingForPlayer}.");
        s.AppendLine();
        s.AppendLine("Allowed actions are:");
        var actions = GameRound.GetAllowedActions();
        var actionNumber = 0;

        foreach (var action in actions)
        {
            actionNumber++;
            s.AppendLine($"   {actionNumber}. {action.ToReadableString()}");
        }

        if (RoundHistory.Count > 0)
        {
            s.AppendLine();
            s.AppendLine("Round history:");
            foreach (var playedAction in RoundHistory)
            {
                s.AppendLine($"   Player {GetPlayerNumber(playedAction.Player)} performed action: {playedAction.Action.ToReadableString()}");
            }
        }

        return s.ToString();
    }

    private int GetPlayerNumber(IPlayer player) =>
        player == Player1 ? 1 : 2; // Note: Only two players are supported

    private static int GetPlayerNumber(PlayerTurn player) =>
        player == PlayerTurn.Player1 ? 1 : 2; // Note: Only two players are supported
}