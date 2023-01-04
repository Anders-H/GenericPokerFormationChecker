using System.Collections.Generic;
using System.Text;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class FormationDescription
{
    public Formation Formation { get; }

    public CardList Cards { get; }

    public int Score { get; }

    internal FormationDescription(Formation formation, IEnumerable<Card> cards, int score)
    {
        Formation = formation;
        Cards = new CardList();
        Cards.AddRange(cards);
        Score = score;
    }

    public override string ToString()
    {
        var s = new StringBuilder();
        s.Append("FORMATION=");
        s.Append(FormationHelper.ToString(Formation));
        s.Append(",SCORE=");
        s.Append(Score.ToString("0000"));
        s.Append(",HAND=");

        if (Cards.Count > 0)
            for (var i = 0; i < Cards.Count; i++)
            {
                s.Append(Cards[i] == null
                    ? "NONE"
                    : Cards[i]?.ToString() ?? "");

                if (Cards[i] != null && Cards[i]!.InFormation)
                    s.Append('*');
                    
                s.Append(i < Cards.Count - 1 ? "-" : "");
            }
        else
            s.Append("NONE");

        return s.ToString();
    }
}