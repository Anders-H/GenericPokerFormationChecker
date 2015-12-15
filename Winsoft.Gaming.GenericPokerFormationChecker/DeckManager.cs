using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    /// <summary>
    ///     Class designed for a cheating computer player in a poker game.
    /// </summary>
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


        private class NewDeckStructure
        {
            private int highestDeck2Score = -1;
            internal string Hand1 { get; set; }
            internal List<Tuple<string, int>> Hands2 { get; set; } = new List<Tuple<string, int>>();
            internal Deck Deck { get; set; }
            internal int HighestDeck2Score
            {
                get
                {
                    if (highestDeck2Score < 0)
                    {
                        var s = 0;
                        Hands2.ForEach(x => { if (x.Item2 > s) s = x.Item2; });
                        highestDeck2Score = s;
                    }
                    return highestDeck2Score;
                }
            }
            public string HighestHand2() => Hands2.Where(x => x.Item2 == HighestDeck2Score).First().Item1;
        }

        /// <summary>
        ///     For a cheating computer player in a poker game, creates a used deck with enough card for one player to make one swap.
        /// </summary>
        /// <param name="secondHandQuality">Cheat level.</param>
        /// <returns></returns>
        public Tuple<string, string> PopHands(int secondHandQuality)
        {
            var deckCount = secondHandQuality > 0 ? (int)(secondHandQuality / 8) : 0;
            var redealCount = secondHandQuality % 8;
            var structures = new List<NewDeckStructure>();
            for (int i = 0; i < deckCount + 1; i++)
            {
                var structure = new NewDeckStructure();
                structure.Deck = new Deck();
                structure.Deck.Shuffle();
                structure.Hand1 = $"{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()}";
                var redeals = (i < deckCount ? 8 : redealCount + 1);
                for (int j = 0; j < redeals; j++)
                {
                    var hand2 = $"{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()}";
                    var fc = new FormationChecker(hand2);
                    fc.CheckFormation();
                    var hand2score = fc.Score;
                    structure.Hands2.Add(Tuple.Create(hand2, hand2score));
                }
                structures.Add(structure);
            }
            structures.Sort((a, b) => (a.HighestDeck2Score > b.HighestDeck2Score ? 1 : (a.HighestDeck2Score < b.HighestDeck2Score ? -1 : 0)));
            this.deck = structures.Last().Deck;
            return Tuple.Create(structures.Last().Hand1, structures.Last().HighestHand2());
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
