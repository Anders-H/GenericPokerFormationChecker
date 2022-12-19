using Winsoft.Gaming.GenericPokerFormationChecker.GameState;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class GameStateTests
{
    [Fact]
    public void PotStaysWhenBothPasses()
    {
        var r = new GameRound();

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player1, Action.Pass);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player2, Action.Pass);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().Count <= 0);
        Assert.True(r.PotStays);

        r.BeginNewRound();

        Assert.True(r.RoundStarter == PlayerTurn.Player2);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player2, Action.Pass);

        Assert.True(r.RoundStarter == PlayerTurn.Player2);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player1, Action.Pass);

        Assert.True(r.RoundStarter == PlayerTurn.Player2);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().Count <= 0);
        Assert.True(r.PotStays);
    }

    [Fact]
    public void CanBetRaiseCall()
    {
        var r = new GameRound();

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player1, Action.Bet);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Call, Action.Raise, Action.Drop));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player2, Action.Raise);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Call, Action.Raise, Action.Drop));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player1, Action.Call);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.ChangeCards));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player2, Action.ChangeCards);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.ChangeCards));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player1, Action.ChangeCards);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Drop, Action.Pass, Action.Bet));
        Assert.False(r.PotStays);
        r.Play(PlayerTurn.Player2, Action.Bet);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player1);
        Assert.True(r.GetAllowedActions().AreOnlyAllowed(Action.Call, Action.Raise, Action.Drop));
        r.Play(PlayerTurn.Player1, Action.Call);
        Assert.False(r.PotStays);

        Assert.True(r.RoundStarter == PlayerTurn.Player1);
        Assert.True(r.WaitingForPlayer == PlayerTurn.Player2);
        Assert.True(r.GetAllowedActions().Count <= 0);
        Assert.False(r.PotStays);
    }
}