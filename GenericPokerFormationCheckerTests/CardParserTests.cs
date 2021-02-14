using Winsoft.Gaming.GenericPokerFormationChecker;
using Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;
using Xunit;

namespace GenericPokerFormationCheckerTests
{
    public class CardParserTests
    {
        [Theory]
        [InlineData("HRT02", Suit.Hearts, Value.Value02)]
        [InlineData("HRT03", Suit.Hearts, Value.Value03)]
        [InlineData("HRT04", Suit.Hearts, Value.Value04)]
        [InlineData("HRT05", Suit.Hearts, Value.Value05)]
        [InlineData("HRT06", Suit.Hearts, Value.Value06)]
        [InlineData("HRT07", Suit.Hearts, Value.Value07)]
        [InlineData("HRT08", Suit.Hearts, Value.Value08)]
        [InlineData("HRT09", Suit.Hearts, Value.Value09)]
        [InlineData("HRT10", Suit.Hearts, Value.Value10)]
        [InlineData("HRTKN", Suit.Hearts, Value.Knight)]
        [InlineData("HRT11", Suit.Hearts, Value.Knight)]
        [InlineData("HRTQU", Suit.Hearts, Value.Queen)]
        [InlineData("HRT12", Suit.Hearts, Value.Queen)]
        [InlineData("HRTKI", Suit.Hearts, Value.King)]
        [InlineData("HRT13", Suit.Hearts, Value.King)]
        [InlineData("HRTAC", Suit.Hearts, Value.Ace)]
        [InlineData("HRT01", Suit.Hearts, Value.Ace)]
        [InlineData("DMN02", Suit.Diamonds, Value.Value02)]
        [InlineData("DMN03", Suit.Diamonds, Value.Value03)]
        [InlineData("DMN04", Suit.Diamonds, Value.Value04)]
        [InlineData("DMN05", Suit.Diamonds, Value.Value05)]
        [InlineData("DMN06", Suit.Diamonds, Value.Value06)]
        [InlineData("DMN07", Suit.Diamonds, Value.Value07)]
        [InlineData("DMN08", Suit.Diamonds, Value.Value08)]
        [InlineData("DMN09", Suit.Diamonds, Value.Value09)]
        [InlineData("DMN10", Suit.Diamonds, Value.Value10)]
        [InlineData("DMNKN", Suit.Diamonds, Value.Knight)]
        [InlineData("DMN11", Suit.Diamonds, Value.Knight)]
        [InlineData("DMNQU", Suit.Diamonds, Value.Queen)]
        [InlineData("DMN12", Suit.Diamonds, Value.Queen)]
        [InlineData("DMNKI", Suit.Diamonds, Value.King)]
        [InlineData("DMN13", Suit.Diamonds, Value.King)]
        [InlineData("DMNAC", Suit.Diamonds, Value.Ace)]
        [InlineData("DMN01", Suit.Diamonds, Value.Ace)]
        [InlineData("CLB02", Suit.Clubs, Value.Value02)]
        [InlineData("CLB03", Suit.Clubs, Value.Value03)]
        [InlineData("CLB04", Suit.Clubs, Value.Value04)]
        [InlineData("CLB05", Suit.Clubs, Value.Value05)]
        [InlineData("CLB06", Suit.Clubs, Value.Value06)]
        [InlineData("CLB07", Suit.Clubs, Value.Value07)]
        [InlineData("CLB08", Suit.Clubs, Value.Value08)]
        [InlineData("CLB09", Suit.Clubs, Value.Value09)]
        [InlineData("CLB10", Suit.Clubs, Value.Value10)]
        [InlineData("CLBKN", Suit.Clubs, Value.Knight)]
        [InlineData("CLB11", Suit.Clubs, Value.Knight)]
        [InlineData("CLBQU", Suit.Clubs, Value.Queen)]
        [InlineData("CLB12", Suit.Clubs, Value.Queen)]
        [InlineData("CLBKI", Suit.Clubs, Value.King)]
        [InlineData("CLB13", Suit.Clubs, Value.King)]
        [InlineData("CLBAC", Suit.Clubs, Value.Ace)]
        [InlineData("CLB01", Suit.Clubs, Value.Ace)]
        [InlineData("SPD02", Suit.Spades, Value.Value02)]
        [InlineData("SPD03", Suit.Spades, Value.Value03)]
        [InlineData("SPD04", Suit.Spades, Value.Value04)]
        [InlineData("SPD05", Suit.Spades, Value.Value05)]
        [InlineData("SPD06", Suit.Spades, Value.Value06)]
        [InlineData("SPD07", Suit.Spades, Value.Value07)]
        [InlineData("SPD08", Suit.Spades, Value.Value08)]
        [InlineData("SPD09", Suit.Spades, Value.Value09)]
        [InlineData("SPD10", Suit.Spades, Value.Value10)]
        [InlineData("SPDKN", Suit.Spades, Value.Knight)]
        [InlineData("SPD11", Suit.Spades, Value.Knight)]
        [InlineData("SPDQU", Suit.Spades, Value.Queen)]
        [InlineData("SPD12", Suit.Spades, Value.Queen)]
        [InlineData("SPDKI", Suit.Spades, Value.King)]
        [InlineData("SPD13", Suit.Spades, Value.King)]
        [InlineData("SPDAC", Suit.Spades, Value.Ace)]
        [InlineData("SPD01", Suit.Spades, Value.Ace)]
        public void Parse(string source, Suit suit, Value value)
        {
            var card = Card.Parse(source);
            Assert.True(card.Suit == suit);
            Assert.True(card.Value == value);
        }

        [Fact]
        public void CantParceUnparcable()
        {
            Assert.Throws<ParseCardFailedException>(() => Card.Parse(null!));
            Assert.Throws<ParseCardFailedException>(() => Card.Parse(""));
            Assert.Throws<ParseCardFailedException>(() => Card.Parse("Sven Hedin"));
        }
    }
}