using System.Linq;

namespace Winsoft.Gaming.GenericPokerFormationChecker.Internal
{
    internal class NewDeckStructure
    {
        private int _highestDeck2Score = -1;

        internal string? Hand1 { get; set; }
        
        internal HandScorePairList Hands2 { get; }
        
        internal Deck? Deck { get; set; }

        internal NewDeckStructure()
        {
            Hands2 = new HandScorePairList();
        }

        internal int HighestDeck2Score
        {
            get
            {
                if (_highestDeck2Score >= 0)
                    return _highestDeck2Score;
                
                var s = 0;

                Hands2.ForEach(x =>
                {
                    if (x.Score > s)
                        s = x.Score;
                });
                
                _highestDeck2Score = s;
                
                return _highestDeck2Score;
            }
        }

        public string HighestHand2() =>
            Hands2.First(x => x.Score == HighestDeck2Score).Hand;
    }
}