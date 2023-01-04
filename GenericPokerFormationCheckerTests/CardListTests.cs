using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class CardListTests
{
    [Fact]
    public void CanCopyList()
    {
        var cards = new[]
        {
            Card.Parse("CLB06"),
            Card.Parse("SPD03"),
            Card.Parse("HRT06")
        };

        var cardList = new CardList(cards);
        
        Assert.True(cardList.Count == 3);
        Assert.True(cardList[0]!.ToString() == "CLB06");
        Assert.True(cardList[1]!.ToString() == "SPD03");
        Assert.True(cardList[2]!.ToString() == "HRT06");
    }

    [Fact]
    public void CanParseHands()
    {
        var x = new CardList();

        var result = x.Parse(@"FORMATION=3-OF-A-KIND,SCORE=0324,HAND=DMN04-DMN05-HRT08*-CLB08*-SPD08*");
        Assert.True(result.Success);
        Assert.Equal(5, x.Count);
        Assert.True(x[0]!.Is(Suit.Diamonds, Value.Value04, false));
        Assert.True(x[1]!.Is(Suit.Diamonds, Value.Value05, false));
        Assert.True(x[2]!.Is(Suit.Hearts, Value.Value08, true));
        Assert.True(x[3]!.Is(Suit.Clubs, Value.Value08, true));
        Assert.True(x[4]!.Is(Suit.Spades, Value.Value08, true));
    }
}