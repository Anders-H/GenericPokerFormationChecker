using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests
{
    public class DeckTests
    {
        [Fact]
        public void CanCreateDeck()
        {
            var deck = new Deck();
            Assert.True(deck.Count == 52);
            Assert.True(deck.PeekString() == "HRT02");
        }

        [Fact]
        public void CanPopCard()
        {
            var deck = new Deck();
            Assert.True(deck.Count == 52);
            Assert.True(deck.PopString() == "HRT02");
            Assert.True(deck.Count == 51);
            Assert.True(deck.PeekString() == "HRT03");
        }

        [Fact]
        public void EmptyDeckGivesEmptyResult()
        {
            var deck = new Deck();
            for (var i = 0; i < 52; i++)
            {
                Assert.True(deck.PopCard() != null);
            }
            Assert.True(deck.PopCard() == null);
            Assert.True(deck.PopString() == "");
        }
    }
}
