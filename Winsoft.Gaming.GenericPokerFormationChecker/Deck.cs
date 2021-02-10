using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class Deck
    {
        private CardList Cards { get; }

        private static Random Rnd { get; }
        
        public int Count =>
            Cards.Count;

        static Deck()
        {
            Rnd = new Random();
        }

        public Deck()
        {
            Cards = new CardList();

            for (var i = 1; i < 5; i++)
                for (var j = 2; j < 15; j++)
                    Cards.Add(Card.Create(i, j));
        }

        public void Shuffle()
        {
            var newDeck = new CardList();
            while (Count > 0)
            {
                var index = Rnd.Next(Count);
                var card = Cards[index];
                Cards.RemoveAt(index);
                newDeck.Add(card);
            }
            Cards.AddRange(newDeck);
        }

        public string Pop()
        {
            if (Count <= 0)
                return "";
            var ret = Cards[0];
            Cards.RemoveAt(0);
            return ret.ToString();
        }

        public string Peek() =>
            Count > 0 ? Cards[0].ToString() : "";
    }
}