namespace Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;

public class TwoPlayerFiveCardStripPokerPlayer : IPlayer
{
    public string Name { get; }
    public bool IsHuman { get; }
    public int Money { get; private set; }
    public int CurrentClothing { get; set; } // 0-3 where 0 is fully clothed and 3 least clothed, 4 is naked
    private string[] ClothingNames { get; }


    public TwoPlayerFiveCardStripPokerPlayer(string name, bool isHuman, string clotheName1, string clotheName2, string clotheName3, string clotheName4)
    {
        Name = name;
        IsHuman = isHuman;
        Money = 100;
        CurrentClothing = 0;
        ClothingNames = [clotheName1, clotheName2, clotheName3, clotheName4];
    }
}