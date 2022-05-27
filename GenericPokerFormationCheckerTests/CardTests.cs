using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class CardTests
{
    [Theory]
    [InlineData(Suit.Hearts, Value.Value02, 200)]
    [InlineData(Suit.Diamonds, Value.Value02, 201)]
    [InlineData(Suit.Clubs, Value.Value02, 202)]
    [InlineData(Suit.Spades, Value.Value02, 203)]
    [InlineData(Suit.Hearts, Value.Ace, 1400)]
    [InlineData(Suit.Diamonds, Value.Ace, 1401)]
    [InlineData(Suit.Clubs, Value.Ace, 1402)]
    [InlineData(Suit.Spades, Value.Ace, 1403)]
    public void SortScore(Suit suit, Value value, int sortScore)
    {
        // Ensure cards are sorted correctly in hand.
        Assert.True(new Card(suit, value).SortScore == sortScore);
    }

    [Fact]
    public void CanCompare()
    {
        var sixOfDiamonds = new Card(Suit.Diamonds, Value.Value06);
        Assert.True(sixOfDiamonds == new Card(Suit.Diamonds, Value.Value06));
        Assert.False(sixOfDiamonds == new Card(Suit.Diamonds, Value.Value07));
    }

    [Fact]
    public void CanParse()
    {
        var suitSource = new[] {"HRT", "DMN", "CLB", "SPD"};
        var suitTarget = new[] {Suit.Hearts, Suit.Diamonds, Suit.Clubs, Suit.Spades};
        var valueSource1 = new[] {"02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "01"};
        var valueSource2 = new[] {"2", "3", "4", "5", "6", "7", "8", "9", "10", "KN", "QU", "KI", "AC", "1"};
        var valueSource3 = new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 1 };
        var targetScore = new[] {2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 14};
        var targetValue = new[]
        {
            Value.Value02, Value.Value03, Value.Value04, Value.Value05, Value.Value06, Value.Value07, Value.Value08,
            Value.Value09, Value.Value10, Value.Knight, Value.Queen, Value.King, Value.Ace, Value.Ace
        };
        var targetValueString = new[] { "02", "03", "04", "05", "06", "07", "08", "09", "10", "KN", "QU", "KI", "AC", "AC" };
        for (var s = 0; s < suitSource.Length; s++)
        {
            for (var v = 0; v < valueSource1.Length; v++)
            {
                var card1 = Card.Create($"{suitSource[s]}{valueSource1[v]}");
                var card2 = Card.Create($"{suitSource[s]}{valueSource2[v]}");
                var card3 = Card.Create(suitTarget[s], targetValue[v]);
                var card4 = Card.Create(suitTarget[s], valueSource3[v]);
                    
                Assert.True(card1.Suit == suitTarget[s]);
                Assert.True(card2.Suit == suitTarget[s]);
                Assert.True(card3.Suit == suitTarget[s]);
                Assert.True(card4.Suit == suitTarget[s]);

                Assert.True(card1.Value == targetValue[v]);
                Assert.True(card2.Value == targetValue[v]);
                Assert.True(card3.Value == targetValue[v]);
                Assert.True(card4.Value == targetValue[v]);

                Assert.True(card1.Score == targetScore[v]);
                Assert.True(card2.Score == targetScore[v]);
                Assert.True(card3.Score == targetScore[v]);
                Assert.True(card4.Score == targetScore[v]);

                Assert.True(card1.ToString() == $"{suitSource[s]}{targetValueString[v]}");
                Assert.True(card2.ToString() == $"{suitSource[s]}{targetValueString[v]}");
                Assert.True(card3.ToString() == $"{suitSource[s]}{targetValueString[v]}");
                Assert.True(card4.ToString() == $"{suitSource[s]}{targetValueString[v]}");
            }
        }
    }

    [Fact]
    public void CanDetermineColor()
    {
        Assert.True(new Card(Suit.Hearts, Value.Value02).Color == Color.Red);
        Assert.True(new Card(Suit.Diamonds, Value.Value03).Color == Color.Red);
        Assert.True(new Card(Suit.Clubs, Value.Value04).Color == Color.Black);
        Assert.True(new Card(Suit.Spades, Value.Value05).Color == Color.Black);
    }
}