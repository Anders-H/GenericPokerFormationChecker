using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{

    internal enum Suit { Hearts, Diamonds, Clubs, Spades }
    internal enum Color { Red, Black }
    internal enum Value { _2 = 2, _3, _4, _5, _6, _7, _8, _9, _10, Knight, Queen, King, Ace }

    internal class Card : IComparable<Card>
    {
        protected bool Equals(Card other) => Suit == other.Suit && Value == other.Value && InFormation == other.InFormation;
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            return obj.GetType() == GetType() && Equals((Card) obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Suit;
                hashCode = (hashCode*397) ^ (int) Value;
                hashCode = (hashCode*397) ^ InFormation.GetHashCode();
                return hashCode;
            }
        }
        public Suit Suit { get; }
        public Value Value { get; }
        public bool InFormation { get; internal set; } = false;
        public Card(Suit suit, Value value)
        {
            Suit = suit;
            Value = value;
        }
        public static Card Parse(string card)
        {
            const string description = "{0} cannot be parsed as a playing card. A card is described as CCCVV where CCC is HRT, DMN, CLB or SPD and VV is 02-10, KN, QU, KI or AC.";
            if (card == null)
                throw new Exceptions.ParseCardFailedException(string.Format(description, "Null"));
            card = card.Trim().ToUpper();
            if (card == "")
                throw new Exceptions.ParseCardFailedException(string.Format(description, "An empty string"));
            if (card.Length != 4 && card.Length != 5)
                throw new Exceptions.ParseCardFailedException(string.Format(description, card));
            var suitString = card.Substring(0, 3);
            Suit suit;
            switch (suitString)
            {
                case "HRT": suit = Suit.Hearts; break;
                case "DMN": suit = Suit.Diamonds; break;
                case "CLB": suit = Suit.Clubs; break;
                case "SPD": suit = Suit.Spades; break;
                default: throw new Exceptions.ParseCardFailedException($"{suitString} cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.");
            }
            var valueString = card.Substring(3);
            Value value;
            switch (valueString)
            {
                case "02": case "2": value = Value._2; break;
                case "03": case "3": value = Value._3; break;
                case "04": case "4": value = Value._4; break;
                case "05": case "5": value = Value._5; break;
                case "06": case "6": value = Value._6; break;
                case "07": case "7": value = Value._7; break;
                case "08": case "8": value = Value._8; break;
                case "09": case "9": value = Value._9; break;
                case "10": value = Value._10; break;
                case "KN": case "11": value = Value.Knight; break;
                case "QU": case "12": value = Value.Queen; break;
                case "KI": case "13": value = Value.King; break;
                case "AC": case "1": case "01": case "14": value = Value.Ace; break;
                default: throw new Exceptions.ParseCardFailedException($"{valueString} cannot be parsed as a value.");
            }
            return new Card(suit, value);
        }
        public static Card Create(Suit suit, Value value) => new Card(suit, value);
        public static Card Create(Suit suit, int twoBasedValue)
        {
            twoBasedValue = twoBasedValue == 1 ? 14 : twoBasedValue;
            if (!(twoBasedValue >= 2 && twoBasedValue <= 14))
                throw new Exception("Valid range: 2-14 or 1.");
            return new Card(suit, (Value)twoBasedValue);
        }
        public static Card Create(int oneBasedSuit, int twoBasedValue)
        {
            twoBasedValue = twoBasedValue == 1 ? 14 : twoBasedValue;
            if (!(twoBasedValue >= 2 && twoBasedValue <= 14))
                throw new Exception("Valid value range: 2-14 or 1.");
            if (!(oneBasedSuit >= 1 && oneBasedSuit <= 4))
                throw new Exception("Valid suit range: 1-4.");
            return new Card((Suit)(oneBasedSuit - 1), (Value)twoBasedValue);
        }
        public Color Color
        {
            get
            {
                switch (Suit)
                {
                    case Suit.Hearts:
                    case Suit.Diamonds:
                        return Color.Red;
                    case Suit.Clubs:
                    case Suit.Spades:
                        return Color.Black;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public int Score => (int) Value;
        internal int SortScore => Score * 100 + (int) Suit;
        public int CompareTo(Card other) => SortScore == other.SortScore ? 0 : SortScore > other.SortScore ? 1 : -1;
        public override string ToString()
        {
            var s = new System.Text.StringBuilder();
            switch (Suit)
            {
                case Suit.Hearts: s.Append("HRT"); break;
                case Suit.Diamonds: s.Append("DMN"); break;
                case Suit.Clubs: s.Append("CLB"); break;
                case Suit.Spades: s.Append("SPD"); break;
                default: throw new ArgumentOutOfRangeException();
            }
            if (Score <= 10)
                s.Append(Score.ToString("00"));
            else
            {
                switch (Score)
                {
                    case 11: s.Append("KN"); break;
                    case 12: s.Append("QU"); break;
                    case 13: s.Append("KI"); break;
                    case 14: s.Append("AC"); break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            return s.ToString();
        }
        public static bool operator ==(Card a, Card b) => a?.Suit == b?.Suit && a?.Value == b?.Value;
        public static bool operator !=(Card a, Card b) => !(a == b);
    }
}
