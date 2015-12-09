using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{

    internal enum Suit { Hearts, Diamonds, Clubs, Spades }
    internal enum Color { Red, Black }
    internal enum Value { _2 = 2, _3, _4, _5, _6, _7, _8, _9, _10, Knight, Queen, King, Ace }

    internal class Card : IComparable<Card>
    {

        public Suit Suit { get; private set; }
        public Value Value { get; private set; }
        public bool InFormation { get; internal set; } = false;

        public Card(Suit suit, Value value)
        {
            this.Suit = suit;
            this.Value = value;
        }

        public static Card Parse(string card)
        {
            var description = "{0} cannot be parsed as a playing card. A card is described as CCCVV where CCC is HRT, DMN, CLB or SPD and VV is 02-10, KN, QU, KI or AC.";
            if (card == null)
                throw new Exceptions.ParseCardFailed(string.Format(description, "Null"));
            card = card.Trim().ToUpper();
            if (card == "")
                throw new Exceptions.ParseCardFailed(string.Format(description, "An empty string"));
            if (!(card.Length == 5))
                throw new Exceptions.ParseCardFailed(string.Format(description, card));
            var suit_string = card.Substring(0, 3);
            var suit = Suit.Hearts;
            switch (suit_string)
            {
                case "HRT": suit = Suit.Hearts; break;
                case "DMN": suit = Suit.Diamonds; break;
                case "CLB": suit = Suit.Clubs; break;
                case "SPD": suit = Suit.Spades; break;
                default: throw new Exceptions.ParseCardFailed(string.Format("{0} cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.", suit_string));
            }
            var value_string = card.Substring(3);
            var value = Value.Ace;
            switch (value_string)
            {
                case "02": value = Value._2; break;
                case "03": value = Value._3; break;
                case "04": value = Value._4; break;
                case "05": value = Value._5; break;
                case "06": value = Value._6; break;
                case "07": value = Value._7; break;
                case "08": value = Value._8; break;
                case "09": value = Value._9; break;
                case "10": value = Value._10; break;
                case "KN": case "11": value = Value.Knight; break;
                case "QU": case "12": value = Value.Queen; break;
                case "KI": case "13": value = Value.King; break;
                case "AC": case "01": case "14": value = Value.Ace; break;
            }
            return new Card(suit, value);
        }

        public static Card Create(Suit suit, Value value)
        {
            return new Card(suit, value);
        }

        public static Card Create(Suit suit, int two_based_value)
        {
            two_based_value = two_based_value == 1 ? 14 : two_based_value;
            if (!(two_based_value >= 2 && two_based_value <= 14))
                throw new Exception("Valid range: 2-14 or 1.");
            return new Card(suit, (Value)two_based_value);
        }

        public static Card Create(int one_based_suit, int two_based_value)
        {
            two_based_value = two_based_value == 1 ? 14 : two_based_value;
            if (!(two_based_value >= 2 && two_based_value <= 14))
                throw new Exception("Valid value range: 2-14 or 1.");
            if (!(one_based_suit >= 1 && one_based_suit <= 4))
                throw new Exception("Valid suit range: 1-4.");
            return new Card((Suit)(one_based_suit - 1), (Value)two_based_value);
        }

        public Color Color
        {
            get
            {
                switch (this.Suit)
                {
                    case Suit.Hearts:
                    case Suit.Diamonds:
                        return Color.Red;
                    default:
                        return Color.Black;
                }
            }
        }

        public int Score { get { return (int)this.Value; } }
        internal int SortScore { get { return (this.Score * 100) + (int)this.Suit; } }

        public int CompareTo(Card other)
        {
            return this.SortScore == other.SortScore ? 0 : this.SortScore > other.SortScore ? 1 : -1;
        }

        public override string ToString()
        {
            var s = new System.Text.StringBuilder();
            switch (this.Suit)
            {
                case Suit.Hearts: s.Append("HRT"); break;
                case Suit.Diamonds: s.Append("DMN"); break;
                case Suit.Clubs: s.Append("CLB"); break;
                default: s.Append("SPD"); break;
            }
            if (this.Score <= 10)
                s.Append(this.Score.ToString("00"));
            else
            {
                switch (this.Score)
                {
                    case 11: s.Append("KN"); break;
                    case 12: s.Append("QU"); break;
                    case 13: s.Append("KI"); break;
                    case 14: s.Append("AC"); break;
                }
            }
            return s.ToString();
        }
    }
}
