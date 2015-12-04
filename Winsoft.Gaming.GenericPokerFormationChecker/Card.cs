using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
   internal class Card : IComparable<Card>
   {
      public enum Suits { Hearts, Diamonds, Clubs, Spades }
      public enum Colors { Red, Black }
      public enum Values { _2 = 2, _3, _4, _5, _6, _7, _8, _9, _10, Knight, Queen, King, Ace }

      public Suits Suit { get; private set; }
      public Values Value { get; private set; }
      public bool InFormation { get; internal set; } = false;

      public Card(Suits suit, Values value)
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
         var suit = Suits.Hearts;
         switch (suit_string)
         {
            case "HRT": suit = Suits.Hearts; break;
            case "DMN": suit = Suits.Diamonds; break;
            case "CLB": suit = Suits.Clubs; break;
            case "SPD": suit = Suits.Spades; break;
            default: throw new Exceptions.ParseSuitFailed($"{suit_string} cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.");
         }
         var value_string = card.Substring(3);
         var value = Values.Ace;
         switch (value_string)
         {
            case "02": value = Values._2; break;
            case "03": value = Values._3; break;
            case "04": value = Values._4; break;
            case "05": value = Values._5; break;
            case "06": value = Values._6; break;
            case "07": value = Values._7; break;
            case "08": value = Values._8; break;
            case "09": value = Values._9; break;
            case "10": value = Values._10; break;
            case "KN": case "11": value = Values.Knight; break;
            case "QU": case "12": value = Values.Queen; break;
            case "KI": case "13": value = Values.King; break;
            case "AC": case "01": case "14": value = Values.Ace; break;
            default: throw new Exceptions.ParseValueFailed($"{value_string} cannot be parsed as a value. A value is described as 02, 03, 04, 05, 06, 07, 08, 09, 10, KN, QU, KI or AC.");
         }

      }

      public static Card Create(Suits suit, Values value) => new Card(suit, value);

      public static Card Create(Suits suit, int two_based_value)
      {
         two_based_value = two_based_value == 1 ? 14 : two_based_value;
         if (!(two_based_value >= 2 && two_based_value <= 14))
            throw new Exception("Valid range: 2-14 or 1.");
         return new Card(suit, (Values)two_based_value);
      }

      public static Card Create(int one_based_suit, int two_based_value)
      {
         two_based_value = two_based_value == 1 ? 14 : two_based_value;
         if (!(two_based_value >= 2 && two_based_value <= 14))
            throw new Exception("Valid value range: 2-14 or 1.");
         if (!(one_based_suit >= 1 && one_based_suit <= 4))
            throw new Exception("Valid suit range: 1-4.");
         return new Card((Suits)(one_based_suit - 1), (Values)two_based_value);
      }

      public Colors Color
      {
         get
         {
            switch (this.Suit)
            {
               case Suits.Hearts:
               case Suits.Diamonds:
                  return Colors.Red;
               default:
                  return Colors.Black;
            }
         }
      }

      public int Score => (int)this.Value;
      internal int SortScore => (this.Score * 100) + (int)this.Suit;
      public int CompareTo(Card other) => this.SortScore == other.SortScore ? 0 : this.SortScore > other.SortScore ? 1 : -1;

      public override string ToString()
      {
         var s = new System.Text.StringBuilder();
         switch (this.Suit)
         {
            case Suits.Hearts: s.Append("HRT"); break;
            case Suits.Diamonds: s.Append("DMN"); break;
            case Suits.Clubs: s.Append("CLB"); break;
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
