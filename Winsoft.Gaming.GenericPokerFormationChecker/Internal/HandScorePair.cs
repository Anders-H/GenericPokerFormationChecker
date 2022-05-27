namespace Winsoft.Gaming.GenericPokerFormationChecker.Internal;

internal class HandScorePair
{
    public string Hand { get; set; }

    public int Score { get; set; }

    public HandScorePair(string hand, int score)
    {
        Hand = hand;
        Score = score;
    }
}