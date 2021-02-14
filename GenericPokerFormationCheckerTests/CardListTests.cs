using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests
{
    public class CardListTests
    {
        [Fact]
        public void CanCopyList()
        {
            var cards = new Card[]
            {
                Card.Parse("CLB06"),
                Card.Parse("SPD03"),
                Card.Parse("HRT06")
            };
            var cardList = new CardList(cards);
            Assert.True(cardList.Count == 3);
            Assert.True(cardList[0].ToString() == "CLB06");
            Assert.True(cardList[1].ToString() == "SPD03");
            Assert.True(cardList[2].ToString() == "HRT06");
        }
    }
}