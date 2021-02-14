using System.Collections.Generic;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class CardList : List<Card>
    {
        public CardList()
        {
        }

        public CardList(IEnumerable<Card>? cards)
        {
            if (cards == null)
                return;

            foreach (var card in cards)
                Add(card);
        }
    }
}