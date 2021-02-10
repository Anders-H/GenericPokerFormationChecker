using System.Collections.Generic;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class CardList : List<Card>
    {
        public CardList()
        {
        }

        public CardList(IEnumerable<Card> cards)
        {
            foreach (var card in cards)
                Add(card);
        }
    }
}