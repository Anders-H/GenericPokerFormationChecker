using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests
{
    public class DeckManagerTests
    {
        [Fact]
        public void CanPopHand()
        {
            var deckManager = new DeckManager();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.True(deckManager.CanPopHand);
            deckManager.PopHand();
            Assert.False(deckManager.CanPopHand);
        }
    }
}