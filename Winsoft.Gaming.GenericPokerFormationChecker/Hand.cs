using System;
using System.Collections.Generic;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

public class Hand : CardList
{
    public Formation Formation { get; set; }
    public int Score { get; set; }

    public Hand()
    {
        Formation = Formation.Nothing;
        Score = 0;
    }

    public Hand(IEnumerable<Card>? cards) : base(cards)
    {
        Formation = Formation.Nothing;
        Score = 0;
        CheckFormation();
    }

    public new bool Parse(string hand)
    {
        Formation = Formation.Nothing;
        Score = 0;

        var result = base.Parse(hand);

        if (!result.Success)
            return false;

        Formation = result.Formation;
        Score = result.Score;
        UniqueOrThrow();
        CheckFormation();

        return true;
    }

    public void Add(Suit suit, Value value)
    {
        Add(new Card(suit, value));
        UniqueOrThrow();
        CheckFormation();
    }


    public void Add(Suit suit, int value)
    {
        Add(Card.Create(suit, value));
        UniqueOrThrow();
        CheckFormation();
    }


    public void Add(string card)
    {
        Add(Card.Parse(card));
        UniqueOrThrow();
        CheckFormation();
    }

    private void UniqueOrThrow()
    {
        foreach (var c in this)
        {
            if (c == null)
                throw new ArgumentOutOfRangeException();
        }

        if (Count <= 0)
            return;

        for (var i = 0; i < Count; i++)
        {
            for (var j = 0; j < Count; j++)
            {
                if (i == j)
                    continue;

                if (this[i] == this[j])
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private void CheckFormation()
    {
        if (Count != 5)
            return;

        var fm = new FormationChecker(this);

        fm.CheckFormation();

        Formation = fm.Formation;
        Score = fm.Score;
    }
}