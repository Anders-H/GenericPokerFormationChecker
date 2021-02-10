using System;
using System.Linq;
using Winsoft.Gaming.GenericPokerFormationChecker.Exceptions;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class FormationChecker
    {
        private readonly Card[] _cards;
        
        internal Formation Formation { get; set; }
        
        internal int Count =>
            _cards.Count(t => !(t == null));

        internal int Score
        {
            get
            {
                var ret = 0;
                for (var i = 0; i < 5; i++)
                    if (!(_cards[i] == null) && _cards[i].InFormation)
                        ret += _cards[i].Score;
                return ret + (int)Formation * 100;
            }
        }

        public FormationChecker(string hand)
        {
            _cards = new Card[5];
            Formation = Formation.Nothing;

            if (hand.IndexOf(',') < 0)
                throw new ParseHandFailedException("A hand is a comma separated string with five cards. Example: SPDAC, SPD02, SPD03, SPD04, SPD05");
            
            var handSplt = hand.Split(',');
            
            if (handSplt.Length != 5)
                throw new ParseHandFailedException("A hand is a comma separated string with five cards. Example: HRT10, HRTKN, HRTQU, HRTKI, HRTAC");
            
            for (var i = 0; i < 5; i++)
            {
                try
                {
                    var card = Card.Parse(handSplt[i].Trim().ToUpper());
                    if (i > 0)
                    {
                        for (var j = 0; j < i; j++)
                            if (_cards[j] == card)
                                throw new DuplicateCardException($"Card {i + 1} ({card}) is a duplicate.");
                    }
                    _cards[i] = card;
                }
                catch (DuplicateCardException)
                {
                    throw;
                }
                catch (ParseCardFailedException ex)
                {
                    var message = ex.Message;
                    throw new ParseCardFailedException($"Failed to parse card {i + 1}/5: {message}");
                }
                catch
                {
                    throw new ParseHandFailedException($"Failed to parse card {i + 1}/5.");
                }
            }
        }

        internal void Clear()
        {
            for (var i = 0; i < _cards.Length; i++)
                _cards[i] = null;
        }
        
        internal int PutCard(Card card)
        {
            var i = GetFirstFreeIndex();
            if (i >= 0 && i < _cards.Length)
                _cards[i] = card;
            return i;
        }

        internal void PutCard(Card card, int index) =>
            _cards[index] = card;

        internal Card PeekCard(int index) =>
            _cards[index];

        internal Card PopCard(int index)
        {
            var ret = _cards[index];
            _cards[index] = null;
            return ret;
        }

        internal CardList PeekCards() =>
            new CardList(
                _cards
                    .Where(t => !(t == null))
                    .ToList()
            );

        internal CardList PopCards()
        {
            var ret = new CardList();
            for (var i = 0; i < _cards.Length; i++)
            {
                if (!(_cards[i] == null))
                    ret.Add(_cards[i]);
                _cards[i] = null;
            }
            return ret;
        }

        internal int GetFirstFreeIndex()
        {
            for (var i = 0; i < _cards.Length; i++)
                if (_cards[i] == null)
                    return i;
            return -1;
        }

        internal int CountSuit(Suit suit) =>
            _cards.Count(x => x.Suit == suit);

        internal int CountValue(Value value) =>
            _cards.Count(x => x.Value == value);

        internal bool Sort()
        {
            if (Count < 5)
                return false;
            Array.Sort(_cards);
            return true;
        }

        internal void Swap(int index1, int index2)
        {
            var temp = _cards[index1];
            _cards[index1] = _cards[index2];
            _cards[index2] = temp;
        }

        internal void ShiftRight()
        {
            var temp = _cards[4];
            for (var i = 3; i >= 0; i--)
                _cards[i + 1] = _cards[i];
            _cards[0] = temp;
        }

        public FormationDescription GetFormationDescription() =>
            new FormationDescription(Formation, _cards, Score);

        public override string ToString() =>
            GetFormationDescription().ToString();

        public bool CheckFormation()
        {
            if (Count < 5)
                return false;
            
            Sort();
            
            //Clear formation.
            Formation = Formation.Nothing;
            foreach (var t in _cards)
                if (!(t == null))
                    t.InFormation = false;

            //Precheck: Straight.
            var isStraight = (_cards[1].Score == _cards[0].Score + 1 &&
                _cards[2].Score == _cards[1].Score + 1 &&
                _cards[3].Score == _cards[2].Score + 1 &&
                _cards[4].Score == _cards[3].Score + 1)
                ||
                (_cards[4].Value == Value.Ace &&
                _cards[0].Value == Value.Value02 &&
                _cards[1].Value == Value.Value03 &&
                _cards[2].Value == Value.Value04 &&
                _cards[3].Value == Value.Value05);

            //Precheck: Flush
            var suit = _cards[0].Suit;
            var isFlush = CountSuit(suit) == 5;

            //In a straight/flush/straight flush: All cards are in formation.
            if (isStraight || isFlush)
                foreach (var t in _cards)
                    t.InFormation = true;

            //Check value representation counts for pairs and so on.
            var valueRepresentations = new int[5];
            for (var i = 0; i < 5; i++)
                valueRepresentations[i] = CountValue(_cards[i].Value);

            //In a wheel straight, swap first and last to make the ace first.
            if (isStraight && _cards[0].Value == Value.Value02 && _cards[4].Value == Value.Ace)
                ShiftRight();

            //Precheck pair count.
            var doubleCount = 0;
            for (var i = 0; i < 5; i++)
                if (valueRepresentations[i] == 2)
                    doubleCount++;
            var twoPairs = doubleCount == 4;
            var onePair = doubleCount == 2;

            //Check royal flush.
            if (isStraight && _cards[0].Value == Value.Value10 && isFlush)
            {
                Formation = Formation.RoyalFlush;
                return true;
            }

            //Check straight flush.
            if (isStraight && isFlush)
            {
                Formation = Formation.StraightFlush;
                return true;
            }

            //Check four of a kind.
            if (valueRepresentations[0] == 4 || valueRepresentations[1] == 4)
            {
                Formation = Formation.FourOfAKind;

                var value = valueRepresentations[0] == 4 ? _cards[0].Value : _cards[1].Value;
                
                for (var i = 0; i < 5; i++)
                    _cards[i].InFormation = _cards[i].Value == value;
                
                return true;
            }

            //Check full house
            if ((valueRepresentations[0] == 2 && valueRepresentations[4] == 3) || (valueRepresentations[0] == 3 && valueRepresentations[4] == 2))
            {
                Formation = Formation.FullHouse;
                foreach (var t in _cards)
                    t.InFormation = true;
                return true;
            }

            //Check flush
            if (isFlush)
            {
                Formation = Formation.Flush;
                return true;
            }

            //Check straight
            if (isStraight)
            {
                Formation = Formation.Straight;
                return true;
            }

            //Check three of a kind
            if (valueRepresentations[0] == 3 || valueRepresentations[2] == 3 || valueRepresentations[4] == 3)
            {
                Formation = Formation.ThreeOfAKind;
                if (valueRepresentations[0] == 3)
                {
                    _cards[0].InFormation = true;
                    _cards[1].InFormation = true;
                    _cards[2].InFormation = true;
                }
                else if (valueRepresentations[4] == 3)
                {
                    _cards[2].InFormation = true;
                    _cards[3].InFormation = true;
                    _cards[4].InFormation = true;
                }
                else
                {
                    _cards[1].InFormation = true;
                    _cards[2].InFormation = true;
                    _cards[3].InFormation = true;
                }
                return true;
            }

            //Check two pairs
            if (twoPairs)
            {
                Formation = Formation.TwoPairs;
                for (var i = 0; i < 5; i++)
                    _cards[i].InFormation = valueRepresentations[i] == 2;
                return true;
            }

            //Check pair
            if (onePair)
            {
                Formation = Formation.Pair;
                for (var i = 0; i < 5; i++)
                    _cards[i].InFormation = valueRepresentations[i] == 2;
                return true;
            }
            Formation = Formation.Nothing;
            _cards[4].InFormation = true;
            return true;
        }
    }
}