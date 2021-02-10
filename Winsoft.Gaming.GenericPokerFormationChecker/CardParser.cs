namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    internal class CardParser
    {
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
            
            var suit = suitString switch
            {
                "HRT" => Suit.Hearts,
                "DMN" => Suit.Diamonds,
                "CLB" => Suit.Clubs,
                "SPD" => Suit.Spades,
                _ => throw new Exceptions.ParseCardFailedException(
                    $"{suitString} cannot be parsed as a suit. A suit is described as HRT, DMN, CLB or SPD.")
            };
            
            var valueString = card.Substring(3);

            var value = valueString switch
            {
                "02" => Value.Value02,
                "2" => Value.Value02,
                "03" => Value.Value03,
                "3" => Value.Value03,
                "04" => Value.Value04,
                "4" => Value.Value04,
                "05" => Value.Value05,
                "5" => Value.Value05,
                "06" => Value.Value06,
                "6" => Value.Value06,
                "07" => Value.Value07,
                "7" => Value.Value07,
                "08" => Value.Value08,
                "8" => Value.Value08,
                "09" => Value.Value09,
                "9" => Value.Value09,
                "10" => Value.Value10,
                "KN" => Value.Knight,
                "11" => Value.Knight,
                "QU" => Value.Queen,
                "12" => Value.Queen,
                "KI" => Value.King,
                "13" => Value.King,
                "AC" => Value.Ace,
                "1" => Value.Ace,
                "01" => Value.Ace,
                "14" => Value.Ace,
                _ => throw new Exceptions.ParseCardFailedException($"{valueString} cannot be parsed as a value.")
            };

            return new Card(suit, value);
        }
    }
}