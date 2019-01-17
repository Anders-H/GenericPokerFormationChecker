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
                case "02": case "2": value = Value.Value02; break;
                case "03": case "3": value = Value.Value03; break;
                case "04": case "4": value = Value.Value04; break;
                case "05": case "5": value = Value.Value05; break;
                case "06": case "6": value = Value.Value06; break;
                case "07": case "7": value = Value.Value07; break;
                case "08": case "8": value = Value.Value08; break;
                case "09": case "9": value = Value.Value09; break;
                case "10": value = Value.Value10; break;
                case "KN": case "11": value = Value.Knight; break;
                case "QU": case "12": value = Value.Queen; break;
                case "KI": case "13": value = Value.King; break;
                case "AC": case "1": case "01": case "14": value = Value.Ace; break;
                default: throw new Exceptions.ParseCardFailedException($"{valueString} cannot be parsed as a value.");
            }
            return new Card(suit, value);
        }
    }
}