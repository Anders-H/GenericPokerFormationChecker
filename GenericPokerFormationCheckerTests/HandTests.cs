using System;
using Winsoft.Gaming.GenericPokerFormationChecker;
using Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class HandTests
{
    [Fact]
    public void CanParseHands()
    {
        var x = new Hand();
        Assert.True(x.Parse(@"FORMATION=3-OF-A-KIND,SCORE=0324,HAND=DMN04-DMN05-HRT08*-CLB08*-SPD08*"));
        Assert.Equal(5, x.Count);
        Assert.Equal(324, x.Score);
        Assert.Equal(Formation.ThreeOfAKind, x.Formation);
        Assert.True(x[0]!.Is(Suit.Diamonds, Value.Value04, false));
        Assert.True(x[1]!.Is(Suit.Diamonds, Value.Value05, false));
        Assert.True(x[2]!.Is(Suit.Hearts, Value.Value08, true));
        Assert.True(x[3]!.Is(Suit.Clubs, Value.Value08, true));
        Assert.True(x[4]!.Is(Suit.Spades, Value.Value08, true));
    }

    [Fact]
    public void CannotAddNullsDuplicates()
    {
        var x = new Hand();
        x.Add(Suit.Diamonds, Value.Value03);
        x.Add(Suit.Diamonds, Value.Value04);
        Assert.Throws<ArgumentOutOfRangeException>(() => x.Add(Suit.Diamonds, Value.Value03));

        var y = new Hand();
        Assert.Throws<ParseCardFailedException>(() => x.Add(null));
    }

[Fact]
public void CanCheckFormations()
{
    var x = new Hand
    {
        { Suit.Clubs, Value.Value02 },
        { Suit.Hearts, Value.Value02 },
        { Suit.Spades, Value.Value02 },
        { Suit.Clubs, Value.Value03 },
        { Suit.Hearts, Value.Value03 }
    };
    Assert.Equal(612, x.Score);
    Assert.Equal(Formation.FullHouse, x.Formation);
}
}