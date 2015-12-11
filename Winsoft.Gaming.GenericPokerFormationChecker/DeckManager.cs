using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class DeckManager
    {
        private Deck deck { get; set; }

        public string Pop()
        {
            CheckDeckCapacity();
            return deck.Pop();
        }


        public string PopHand()
        {
            if (!(CanPopHand))
            {
                deck = new Deck();
                deck.Shuffle();
            }
            string[] cards = { Pop(), Pop(), Pop(), Pop(), Pop() };
            var s = new StringBuilder();
            for (int i = 0; i < 5; i++)
                s.Append(cards[i].ToString() + (i < 4 ? ", " : ""));
            return s.ToString();
        }

        public Tuple<string, string> PopHands(int secondHandQuality)
        {
            //TODO: Read quality parameter.
            return Tuple.Create(PopHand(), PopHand());
        }

        public bool CanPopHand { get { return Count >= 5; } }

        public int Count { get { CheckDeckCapacity(); return deck.Count; } }

        private void CheckDeckCapacity()
        {
            if (deck == null || deck.Count <= 0)
            {
                deck = new Deck();
                deck.Shuffle();
            }
        }
    }
}
