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
