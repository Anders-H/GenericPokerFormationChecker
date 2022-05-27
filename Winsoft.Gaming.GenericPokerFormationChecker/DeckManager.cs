using System.Collections.Generic;
using System.Linq;
using Winsoft.Gaming.GenericPokerFormationChecker.Internal;

namespace Winsoft.Gaming.GenericPokerFormationChecker;

/// <summary>
///     Class designed for a cheating computer player in a poker game.
/// </summary>
public class DeckManager
{
    private Deck? Deck { get; set; }

    public bool CanPopHand =>
        Count >= 5;

    public int Count
    {
        get
        {
            CheckDeckCapacity();
            return Deck?.Count ?? 0;
        }
    }

    public string Pop()
    {
        CheckDeckCapacity();
        return Deck?.PopString() ?? "";
    }

    public string PopHand()
    {
        if (!CanPopHand)
        {
            Deck = new Deck();
            Deck.Shuffle();
        }
        string[] cards =
        {
            Pop(),
            Pop(),
            Pop(),
            Pop(),
            Pop()
        };

        return string.Join(", ", cards);
    }

    /// <summary>
    ///     For a cheating computer player in a poker game, creates a used deck with enough cards left for one player to make one swap.
    /// </summary>
    /// <param name="secondHandQuality">Cheat level.</param>
    /// <returns></returns>
    public HandPair PopHands(int secondHandQuality)
    {
        var deckCount = secondHandQuality > 0 ? secondHandQuality / 8 : 0;
        var redealCount = secondHandQuality % 8;
        var structures = new List<NewDeckStructure>();
        for (var i = 0; i < deckCount + 1; i++)
        {
            var structure = new NewDeckStructure
            {
                Deck = new Deck()
            };

            var d = structure.Deck;
            structure.Deck.Shuffle();
            structure.Hand1 = $"{d.PopString()},{d.PopString()},{d.PopString()},{d.PopString()},{d.PopString()}";
            var redeals = i < deckCount ? 8 : redealCount + 1;
            for (var j = 0; j < redeals; j++)
            {
                var hand2 = $"{d.PopString()},{d.PopString()},{d.PopString()},{d.PopString()},{d.PopString()}";
                    
                var fc = new FormationChecker(hand2);
                    
                fc.CheckFormation();
                    
                var hand2Score = fc.Score;
                    
                structure.Hands2.Add(
                    new HandScorePair(hand2, hand2Score)
                );
            }
            structures.Add(structure);
        }

        structures.Sort(
            (a, b) => a.HighestDeck2Score > b.HighestDeck2Score
                ? 1
                : a.HighestDeck2Score < b.HighestDeck2Score ? -1 : 0
        );
            
        Deck = structures.Last().Deck;

        return new HandPair(
            structures.Last().Hand1 ?? "",
            structures.Last().HighestHand2()
        );
    }

    private void CheckDeckCapacity()
    {
        if (Deck is { Count: > 0 })
            return;

        Deck = new Deck();
        Deck.Shuffle();
    }
}