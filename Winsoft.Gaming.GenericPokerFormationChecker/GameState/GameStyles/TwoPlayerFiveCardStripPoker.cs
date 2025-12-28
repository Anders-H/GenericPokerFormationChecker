using System;
using System.Text;

namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;

public class TwoPlayerFiveCardStripPoker
{
    public static readonly Random Random;
    public TwoPlayerFiveCardStripPokerPlayer Player1 { get; }
    public TwoPlayerFiveCardStripPokerPlayer Player2 { get; }
    public DeckManager? DeckManager { get; private set; }
    public GameRound? GameRound { get; private set; }
    public PlayedActionList RoundHistory { get; private set; }
    public ComputerPlayerSkillLevel ComputerSkillLevel { get; private set; }

    static TwoPlayerFiveCardStripPoker()
    {
        Random = new Random();
    }

    public TwoPlayerFiveCardStripPoker(TwoPlayerFiveCardStripPokerPlayer humanPlayer, TwoPlayerFiveCardStripPokerPlayer computerPlayer)
    {
        Player1 = humanPlayer;
        Player2 = computerPlayer;
        ComputerSkillLevel = ComputerPlayerSkillLevel.Mediocre;
    }

    public GameRound NextRound()
    {
        switch (Player2.CurrentClothing)
        {
            case 1:
                ComputerSkillLevel = ComputerPlayerSkillLevel.Mediocre;
                break;
            case 2:
                ComputerSkillLevel = ComputerPlayerSkillLevel.Good;
                break;
            case 3:
                ComputerSkillLevel = ComputerPlayerSkillLevel.Expert;
                break;
            default:
                ComputerSkillLevel = ComputerPlayerSkillLevel.Useless;
                break;
        }

        RoundHistory = [];
        DeckManager = new DeckManager();
        var hands = DeckManager.PopHands(ComputerSkillLevel);
        Player1.Hand = new Hand(hands.Hand1);
        Player2.Hand = new Hand(hands.Hand2);
        GameRound ??= new GameRound();
        GameRound.BeginNewRound();
        return GameRound;
    }

    public override string ToString() =>
        ToString(GameRound?.GetAllowedActions() ?? []);

    public string ToString(ActionList actions)
    {
        if (GameRound == null)
            return "Game is not started.";

        var s = new StringBuilder();
        s.AppendLine($"Round stated by {GameRound.RoundStarter} (player {GetPlayerNumber(GameRound.WaitingForPlayer)}).");
        s.AppendLine($"Current player is {GameRound.WaitingForPlayer}.");
        s.AppendLine();
        s.AppendLine("Allowed actions are:");
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

        s.AppendLine();
        s.AppendLine($"{Player1.Name}: ${Player1.Money}, {Player2.Name}: ${Player2.Money}");
        s.AppendLine();
        s.AppendLine("Your hand and computer's hand:");
        s.AppendLine(Player1.Hand?.ToString());
        s.AppendLine(Player2.Hand?.ToString());
        return s.ToString();
    }

    private int GetPlayerNumber(IPlayer player) =>
        player == Player1 ? 1 : 2; // Note: Only two players are supported

    private static int GetPlayerNumber(PlayerTurn player) =>
        player == PlayerTurn.Player1 ? 1 : 2; // Note: Only two players are supported

    public bool ActionAllowed(PlayedAction action) =>
        GameRound != null && GameRound.ActionAllowed(action.Action);

    public bool ActionAllowed(Action action) =>
        GameRound != null && GameRound.ActionAllowed(action);

    public Action GetComputerAction()
    {
        // TODO: Create some intelligence here.
        var actions = GameRound?.GetAllowedActions() ?? [];

        if (actions.Count <= 0)
            throw new InvalidOperationException("No allowed actions for computer player.");

        var index = Random.Next(0, actions.Count);
        return actions[index];
    }
}