using System;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class FormationHelper
{
    public static string ToString(Formation formation) =>
        formation switch
        {
            Formation.Pair => "PAIR",
            Formation.TwoPairs => "2-PAIRS",
            Formation.ThreeOfAKind => "3-OF-A-KIND",
            Formation.Straight => "STRAIGHT",
            Formation.Flush => "FLUSH",
            Formation.FullHouse => "FULL-HOUSE",
            Formation.FourOfAKind => "4-OF-A-KIND",
            Formation.StraightFlush => "STRAIGHT-FLUSH",
            Formation.RoyalFlush => "ROYAL-FLUSH",
            Formation.Nothing => "NOTHING",
            _ => throw new ArgumentOutOfRangeException()
        };

    public static Formation FromString(string formation) =>
        formation.ToUpper() switch
        {
            "PAIR" => Formation.Pair,
            "2-PAIRS" => Formation.TwoPairs,
            "3-OF-A-KIND" => Formation.ThreeOfAKind,
            "STRAIGHT" => Formation.Straight,
            "FLUSH" => Formation.Flush,
            "FULL-HOUSE" => Formation.FullHouse,
            "4-OF-A-KIND" => Formation.FourOfAKind,
            "STRAIGHT-FLUSH" => Formation.StraightFlush,
            "ROYAL-FLUSH" => Formation.RoyalFlush,
            "NOTHING" => Formation.Nothing,
            _ => throw new ArgumentOutOfRangeException()
        };
}