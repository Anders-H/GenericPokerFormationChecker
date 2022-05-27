using System;
using System.ComponentModel;
using System.Text;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class Card : IComparable<Card>
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int SortScore =>
        Score * 100 + (int)Suit;

    public Suit Suit { get; }

    public Value Value { get; }

    public bool InFormation { get; internal set; } = false;

    public int Score =>
        (int)Value;

    public Card(Suit suit, Value value)
    {
        Suit = suit;
        Value = value;
    }

    protected bool Equals(Card other) =>
        Suit == other.Suit && Value == other.Value && InFormation == other.InFormation;

    public static Card Create(Suit suit, Value value) =>
        new(suit, value);

    public static Card Create(Suit suit, int value)
    {
        value = value == 1 ? 14 : value;
        
        if (!(value is >= 2 and <= 14))
            throw new Exception("Valid range: 2-14 or 1.");
        
        return new Card(suit, (Value)value);
    }

    public static Card Create(int oneBasedSuit, int value)
    {
        value = value == 1 ? 14 : value;

        if (!(value is >= 2 and <= 14))
            throw new Exception("Valid value range: 2-14 or 1.");
        
        if (!(oneBasedSuit is >= 1 and <= 4))
            throw new Exception("Valid suit range: 1-4.");
        
        return new Card((Suit)(oneBasedSuit - 1), (Value)value);
    }

    public static Card Create(string card) =>
        CardParser.Parse(card);

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;

        if (ReferenceEquals(this, obj))
            return true;
        
        return obj.GetType() == GetType() && Equals((Card)obj);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = (int)Suit;
            hashCode = (hashCode * 397) ^ (int)Value;
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            hashCode = (hashCode * 397) ^ InFormation.GetHashCode();
            return hashCode;
        }
    }

    public static Card Parse(string card) =>
        CardParser.Parse(card);

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

    public int CompareTo(Card? other) =>
        SortScore == (other?.SortScore ?? 0) ? 0 : SortScore > (other?.SortScore ?? 0) ? 1 : -1;

    public override string ToString()
    {
        var s = new StringBuilder();

        switch (Suit)
        {
            case Suit.Hearts:
                s.Append("HRT");
                break;
            case Suit.Diamonds:
                s.Append("DMN");
                break;
            case Suit.Clubs:
                s.Append("CLB");
                break;
            case Suit.Spades:
                s.Append("SPD");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (Score <= 10)
            s.Append(Score.ToString("00"));
        else
        {
            switch (Score)
            {
                case 11:
                    s.Append("KN");
                    break;
                case 12:
                    s.Append("QU");
                    break;
                case 13:
                    s.Append("KI");
                    break;
                case 14:
                    s.Append("AC");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return s.ToString();
    }

    public static bool operator ==(Card? a, Card? b) =>
        a?.Suit == b?.Suit && a?.Value == b?.Value;

    public static bool operator !=(Card? a, Card? b) =>
        !(a == b);
}