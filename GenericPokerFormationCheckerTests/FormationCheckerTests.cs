using Winsoft.Gaming.GenericPokerFormationChecker;
using Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class FormationCheckerTests
{
    [Fact]
    public void ExceptionOnFailedParsing()
    {
        // No comma.
        Assert.Throws<ParseHandFailedException>(() => new FormationChecker("Hello"));
        // Too few cards.
        Assert.Throws<ParseHandFailedException>(() => new FormationChecker("HRT02,HRT03,HRT04,HRT05"));
        // Too many cards.
        Assert.Throws<ParseHandFailedException>(() => new FormationChecker("HRT02,HRT03,HRT04,HRT05,HRT06,HRT07"));
        // Duplicate card.
        Assert.Throws<DuplicateCardException>(() => new FormationChecker("HRT02,HRT03,HRT04,HRT05,HRT05"));
        // Cannot parse first card.
        Assert.Throws<ParseCardFailedException>(() => new FormationChecker("HRP02,HRT03,HRT04,HRT05,HRT06"));
    }

    [Theory]
    [InlineData("SPD02,DMNAC,CLB04,HRT09,HRT06", Formation.Nothing)]
    [InlineData("SPD02,DMN02,CLB04,HRT09,HRT06", Formation.Pair)]
    [InlineData("SPD02,DMN02,CLB04,HRT04,HRT06", Formation.TwoPairs)]
    [InlineData("SPD02,DMN02,CLB02,HRT09,HRT06", Formation.ThreeOfAKind)]
    [InlineData("SPD02,DMNAC,CLB05,HRT03,HRT04", Formation.Straight)]
    [InlineData("SPD02,SPD10,SPD05,SPD09,SPD04", Formation.Flush)]
    [InlineData("SPD02,HRT02,SPD03,HRT03,CLB02", Formation.FullHouse)]
    [InlineData("HRT02,HRT10,SPD02,CLB02,DMN02", Formation.FourOfAKind)]
    [InlineData("SPD02,SPDAC,SPD05,SPD03,SPD04", Formation.StraightFlush)]
    [InlineData("CLBKN,CLB10,CLBKI,CLBQU,CLBAC", Formation.RoyalFlush)]
    public void CanDetectFormation(string source, Formation target)
    {
        var formationChecker = new FormationChecker(source);
        formationChecker.CheckFormation();
        Assert.True(formationChecker.GetFormationDescription().Formation == target);
    }
}