using System.Collections.Generic;
using System.Globalization;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class CardList : List<Card?>
{
    public CardList()
    {
    }

    public CardList(IEnumerable<Card?>? cards)
    {
        if (cards == null)
            return;

        foreach (var card in cards)
            if (card != null)
                Add(card);
    }

    public CardListParseResult Parse(string hand)
    {
        Clear();

        try
        {
            var outerParts = hand.Split(',');
            var formationRaw = outerParts[0];
            var scoreRaw = outerParts[1];
            var cardsRaw = outerParts[2];

            var cards = cardsRaw.Split('-');
            cards[0] = cards[0].Split('=')[1];

            if (cards.Length != 5)
                return CardListParseResult.Fail();

            for (var i = 0; i < 5; i++)
                Add(Card.Parse(cards[i]));

            var formation = FormationHelper.FromString(formationRaw.Split('=')[1]);
            var score = ParseScore(scoreRaw);

            return new CardListParseResult(true, formation, score);
        }
        catch
        {
            return CardListParseResult.Fail();
        }
    }

    private int ParseScore(string raw)
    {
        var parts = raw.Split('=');
        return int.Parse(parts[1], NumberStyles.Any, CultureInfo.InvariantCulture);
    }
}