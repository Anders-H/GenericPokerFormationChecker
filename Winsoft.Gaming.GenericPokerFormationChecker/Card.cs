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
      public bool InFormation { get; internal set; }

      public Card(Suits suit, Values value)
      {
         this.Suit = suit;
         this.Value = value;
         this.InFormation = false;
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
         switch(suit_string)
         {
            case "HRT":
            case "DMN":
            case "CLB":
            case "SPD":
            default:
               throw new Exceptions.ParseCardFailed(string.Format("{0} cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.", suit_string));
         }
         var value_strign = card.Substring(3);

      }

      public static Card Create(Suits suit, Values value)
      {
         return new Card(suit, value);
      }

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
