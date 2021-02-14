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

        [Fact]
        public void CanProduceAdjustableQualityOnSecondHand()
        {
            const int samplePoints = 10000;

            var hand1Score = 0.0;
            var hand2Score = 0.0;
            
            for (var x = 0; x < samplePoints; x++)
            {
                var deckManager = new DeckManager();
                var hands = deckManager.PopHands(10);

                var fc1 = new FormationChecker(hands.Hand1);
                Assert.True(fc1.CheckFormation(out var score1));
                hand1Score += score1;

                var fc2 = new FormationChecker(hands.Hand2);
                Assert.True(fc2.CheckFormation(out var score2));
                hand2Score += score2;
            }

            hand1Score /= samplePoints;
            hand2Score /= samplePoints;

            Assert.True(hand1Score > 0 && hand1Score <= 100);
            Assert.True(hand2Score > 150 && hand2Score <= 250);

            hand1Score = 0.0;
            hand2Score = 0.0;

            for (var x = 0; x < samplePoints; x++)
            {
                var deckManager = new DeckManager();
                var hands = deckManager.PopHands(100);

                var fc1 = new FormationChecker(hands.Hand1);
                Assert.True(fc1.CheckFormation(out var score1));
                hand1Score += score1;

                var fc2 = new FormationChecker(hands.Hand2);
                Assert.True(fc2.CheckFormation(out var score2));
                hand2Score += score2;
            }

            hand1Score /= samplePoints;
            hand2Score /= samplePoints;

            Assert.True(hand1Score > 0 && hand1Score <= 100);
            Assert.True(hand2Score > 350 && hand2Score <= 450);
        }
    }
}