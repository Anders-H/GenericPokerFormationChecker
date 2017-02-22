using System;
using System.Collections.Generic;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class Deck
    {
        private List<Card> Cards { get; } = new List<Card>();
        private static Random Rnd { get; } = new Random();
        public Deck()
        {
            for (var i = 1; i < 5; i++)
                for (var j = 2; j < 15; j++)
                    Cards.Add(Card.Create(i, j));
        }
        public void Shuffle()
        {
            var newDeck = new List<Card>();
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
        public string Peek() => Count > 0 ? Cards[0].ToString() : "";
        public int Count => Cards.Count;
    }
}
