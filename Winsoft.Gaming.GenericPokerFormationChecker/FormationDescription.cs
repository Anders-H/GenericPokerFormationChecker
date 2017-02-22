using System;
using System.Collections.Generic;
using System.Text;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class FormationDescription
    {
        internal FormationDescription(Formation formation, IEnumerable<Card> cards, int score)
        {
            Formation = formation;
            Cards = new List<Card>();
            Cards.AddRange(cards);
            Score = score;
        }
        public Formation Formation { get; }
        public List<Card> Cards { get; }
        public int Score { get; }
        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append("FORMATION=");
            switch (Formation)
            {
                case Formation.Pair:
                    s.Append("PAIR");
                    break;
                case Formation.TwoPairs:
                    s.Append("2-PAIRS");
                    break;
                case Formation.ThreeOfAKind:
                    s.Append("3-OF-A-KIND");
                    break;
                case Formation.Straight:
                    s.Append("STRAIGHT");
                    break;
                case Formation.Flush:
                    s.Append("FLUSH");
                    break;
                case Formation.FullHouse:
                    s.Append("FULL-HOUSE");
                    break;
                case Formation.FourOfAKind:
                    s.Append("4-OF-A-KIND");
                    break;
                case Formation.StraightFlush:
                    s.Append("STRAIGHT-FLUSH");
                    break;
                case Formation.RoyalFlush:
                    s.Append("ROYAL-FLUSH");
                    break;
                case Formation.Nothing:
                    s.Append("NOTHING");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            s.Append(",SCORE=");
            s.Append(Score.ToString("0000"));
            s.Append(",HAND=");
            if (Cards.Count > 0)
                for (var i = 0; i < Cards.Count; i++)
                {
                    s.Append(Cards[i] == null ? "NONE" : Cards[i].ToString());
                    if (!(Cards[i] == null) && Cards[i].InFormation)
                        s.Append("*");
                    s.Append(i < Cards.Count - 1 ? "-" : "");
                }
            else
                s.Append("NONE");
            return s.ToString();
        }
    }
}
