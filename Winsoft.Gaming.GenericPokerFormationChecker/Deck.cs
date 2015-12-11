using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class Deck
    {
        private List<Card> cards { get; } = new List<Card>();
        private static Random rnd { get; } = new Random();

        public Deck()
        {
            for (int i = 1; i < 5; i++)
                for (int j = 2; j < 15; j++)
                    cards.Add(Card.Create(i, j));
        }

        public void Shuffle()
        {
            var new_deck = new List<Card>();
            while (Count > 0)
            {
                var index = rnd.Next(Count);
                var card = cards[index];
                cards.RemoveAt(index);
                new_deck.Add(card);
            }
            cards.AddRange(new_deck);
        }

        public string Pop()
        {
            if (Count > 0)
            {
                var ret = cards[0];
                cards.RemoveAt(0);
                return ret.ToString();
            }
            return "";
        }

        public string Peek()
        {
            if (Count > 0)
                return cards[0].ToString();
            return "";
        }

        public int Count { get { return cards.Count; } }
    }
}
