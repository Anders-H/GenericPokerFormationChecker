namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class CardListParseResult
{
    public bool Success { get; }
    public Formation Formation { get; }
    public int Score { get; }

    public CardListParseResult(bool success, Formation formation, int score)
    {
        Success = success;
        Formation = formation;
        Score = score;
    }

    public static CardListParseResult Fail() =>
        new(false, Formation.Nothing, 0);
}