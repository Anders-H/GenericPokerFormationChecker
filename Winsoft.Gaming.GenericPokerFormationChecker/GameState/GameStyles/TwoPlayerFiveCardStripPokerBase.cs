namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;

public abstract class TwoPlayerFiveCardStripPokerBase
{
    public IPlayer Player1 { get; }
    public IPlayer Player2 { get; }
    public GameRound? GameRound { get; }

    protected TwoPlayerFiveCardStripPokerBase(IPlayer humanPlayer, IPlayer computerPlayer)
    {
        Player1 = humanPlayer;
        Player2 = computerPlayer;
    }

    public virtual void NextRound()
    {

    }
}