using System.Collections.Generic;
using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    /// <summary>
    ///     Class designed for a cheating computer player in a poker game.
    /// </summary>
    public class DeckManager
    {
        private Deck Deck { get; set; }
        public string Pop()
        {
            CheckDeckCapacity();
            return Deck.Pop();
        }
        public string PopHand()
        {
            if (!CanPopHand)
            {
                Deck = new Deck();
                Deck.Shuffle();
            }
            string[] cards = { Pop(), Pop(), Pop(), Pop(), Pop() };
            return string.Join(", ", cards);
        }
        private class NewDeckStructure
        {
            private int _highestDeck2Score = -1;
            internal string Hand1 { get; set; }
            internal List<(string hand, int score)> Hands2 { get; } = new List<(string, int)>();
            internal Deck Deck { get; set; }
            internal int HighestDeck2Score
            {
                get
                {
                    if (_highestDeck2Score >= 0)
                        return _highestDeck2Score;
                    var s = 0;
                    Hands2.ForEach(x => { if (x.score > s) s = x.score; });
                    _highestDeck2Score = s;
                    return _highestDeck2Score;
                }
            }
            public string HighestHand2() => Hands2.First(x => x.score == HighestDeck2Score).hand;
        }
        /// <summary>
        ///     For a cheating computer player in a poker game, creates a used deck with enough cards left for one player to make one swap.
        /// </summary>
        /// <param name="secondHandQuality">Cheat level.</param>
        /// <returns></returns>
        public (string hand1, string hand2) PopHands(int secondHandQuality)
        {
            var deckCount = secondHandQuality > 0 ? secondHandQuality / 8 : 0;
            var redealCount = secondHandQuality % 8;
            var structures = new List<NewDeckStructure>();
            for (var i = 0; i < deckCount + 1; i++)
            {
                var structure = new NewDeckStructure();
                structure.Deck = new Deck();
                structure.Deck.Shuffle();
                structure.Hand1 = $"{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()}";
                var redeals = i < deckCount ? 8 : redealCount + 1;
                for (var j = 0; j < redeals; j++)
                {
                    var hand2 = $"{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()},{structure.Deck.Pop()}";
                    var fc = new FormationChecker(hand2);
                    fc.CheckFormation();
                    var hand2Score = fc.Score;
                    structure.Hands2.Add((hand: hand2, score: hand2Score));
                }
                structures.Add(structure);
            }
            structures.Sort((a, b) => a.HighestDeck2Score > b.HighestDeck2Score ? 1 : (a.HighestDeck2Score < b.HighestDeck2Score ? -1 : 0));
            Deck = structures.Last().Deck;
            return (hand1: structures.Last().Hand1, hand2: structures.Last().HighestHand2());
        }
        public bool CanPopHand => Count >= 5;
        public int Count { get { CheckDeckCapacity(); return Deck.Count; } }
        private void CheckDeckCapacity()
        {
            if (Deck != null && Deck.Count > 0)
                return;
            Deck = new Deck();
            Deck.Shuffle();
        }
    }
}
