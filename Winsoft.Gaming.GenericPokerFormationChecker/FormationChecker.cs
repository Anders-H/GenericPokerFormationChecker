using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    internal enum Formation
    {
        Nothing, Pair, TwoPairs, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush
    }

    public class FormationChecker
    {
        public FormationChecker(string hand)
        {
            this.Cards = new Card[5];
            this.Formation = Formation.Nothing;
            if (hand.IndexOf(',') < 0)
                throw new Exceptions.ParseHandFailed("A hand is a comma separated string with five cards. Example: SPDAC, SPD02, SPD03, SPD04, SPD05");
            var hand_splt = hand.Split(',');
            if (!(hand_splt.Length == 5))
                throw new Exceptions.ParseHandFailed("A hand is a comma separated string with five cards. Example: HRT10, HRTKN, HRTQU, HRTKI, HRTAC");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    this.Cards[i] = Card.Parse(hand_splt[i].Trim().ToUpper());
                }
                catch (Exceptions.ParseCardFailed ex)
                {
                    var message = ex.Message;
                    throw new Exceptions.ParseCardFailed($"Failed to parse card {(i + 1)}/5: {message}");
                }
                catch
                {
                    throw new Exceptions.ParseHandFailed($"Failed to parse card {(i + 1)}/5.");
                }
            }
        }

        private Card[] Cards;

        internal Formation Formation { get; set; }

        internal int Score
        {
            get
            {
                var ret = 0;
                for (int i = 0; i < 5; i++)
                    if (!(this.Cards[i] == null) && this.Cards[i].InFormation)
                        ret += this.Cards[i].Score;
                return ret + ((int)this.Formation * 100);
            }
        }

        internal void Clear()
        {
            for (int i = 0; i < this.Cards.Length; i++)
                this.Cards[i] = null;
        }

        internal void ClearFormation()
        {
            this.Formation = Formation.Nothing;
            for (int i = 0; i < this.Cards.Length; i++)
                if (!(this.Cards[i] == null))
                    this.Cards[i].InFormation = false;
        }

        internal int Count
        {
            get
            {
                var ret = 0;
                for (int i = 0; i < this.Cards.Length; i++)
                    if (!(this.Cards[i] == null))
                        ret++;
                return ret;
            }
        }

        internal int PutCard(Card card)
        {
            var i = this.GetFirstFreeIndex();
            if (i >= 0 && i < this.Cards.Length)
                this.Cards[i] = card;
            return i;
        }

        internal void PutCard(Card card, int index)
        {
            this.Cards[index] = card;
        }

        internal Card PeekCard(int index)
        {
            return this.Cards[index];
        }

        internal Card PopCard(int index)
        {
            var ret = this.Cards[index];
            this.Cards[index] = null;
            return ret;
        }

        internal List<Card> PeekCards()
        {
            var ret = new List<Card>();
            for (int i = 0; i < this.Cards.Length; i++)
                if (!(this.Cards[i] == null))
                    ret.Add(this.Cards[i]);
            return ret;
        }
        internal List<Card> PopCards()
        {
            var ret = new List<Card>();
            for (int i = 0; i < this.Cards.Length; i++)
            {
                if (!(this.Cards[i] == null))
                    ret.Add(this.Cards[i]);
                this.Cards[i] = null;
            }
            return ret;
        }

        internal int GetFirstFreeIndex()
        {
            for (int i = 0; i < this.Cards.Length; i++)
                if (this.Cards[i] == null)
                    return i;
            return -1;
        }

        internal int CountSuit(Suit suit)
        {
            return this.Cards.Where(x => x.Suit == suit).Count();
        }

        internal int CountValue(Value value)
        {
            return this.Cards.Where(x => x.Value == value).Count();
        }

        internal bool Sort()
        {
            if (this.Count >= 5)
            {
                Array.Sort(this.Cards);
                return true;
            }
            return false;
        }

        internal void Swap(int index1, int index2)
        {
            Card temp = this.Cards[index1];
            this.Cards[index1] = this.Cards[index2];
            this.Cards[index2] = temp;
        }

        internal void ShiftRight()
        {
            Card temp = this.Cards[4];
            for (int i = 3; i >= 0; i--)
                this.Cards[i + 1] = this.Cards[i];
            this.Cards[0] = temp;
        }

        public override string ToString()
        {
            var s = new System.Text.StringBuilder();
            s.Append("FORMATION=");
            switch (this.Formation)
            {
                case Formation.Pair:
                    s.Append("PAIR");
                    break;
                case Formation.TwoPairs:
                    s.Append("2-PAIRS");
                    break;
                case Formation.ThreeOfAKind:
                    s.Append("3-OF-A-KIND");
                    break;
                case Formation.Straight:
                    s.Append("STRAIGHT");
                    break;
                case Formation.Flush:
                    s.Append("FLUSH");
                    break;
                case Formation.FullHouse:
                    s.Append("FULL-HOUSE");
                    break;
                case Formation.FourOfAKind:
                    s.Append("4-OF-A-KIND");
                    break;
                case Formation.StraightFlush:
                    s.Append("STRAIGHT-FLUSH");
                    break;
                case Formation.RoyalFlush:
                    s.Append("ROYAL-FLUSH");
                    break;
                default:
                    s.Append("NOTHING");
                    break;
            }
            s.Append(",SCORE=");
            s.Append(this.Score.ToString("0000"));
            s.Append(",HAND=");
            for (int i = 0; i < 5; i++)
            {
                s.Append(this.Cards[i] == null ? "NONE" : this.Cards[i].ToString());
                if (!(this.Cards[i] == null) && this.Cards[i].InFormation)
                    s.Append("*");
                s.Append(i < 4 ? "-" : "");
            }
            return s.ToString();
        }

        public bool CheckFormation()
        {
            if (this.Count < 5)
                return false;
            this.Sort();
            this.ClearFormation();

            //Precheck: Straight.
            var is_straight = (this.Cards[1].Score == this.Cards[0].Score + 1 &&
                           this.Cards[2].Score == this.Cards[1].Score + 1 &&
                           this.Cards[3].Score == this.Cards[2].Score + 1 &&
                           this.Cards[4].Score == this.Cards[3].Score + 1)
                           ||
                           (this.Cards[4].Value == Value.Ace &&
                           this.Cards[0].Value == Value._2 &&
                           this.Cards[1].Value == Value._3 &&
                           this.Cards[2].Value == Value._4 &&
                           this.Cards[3].Value == Value._5);

            //Precheck: Flush
            var suit = this.Cards[0].Suit;
            var is_flush = this.CountSuit(suit) == 5;

            //In a straight/flush/straight flush: All cards are in formation.
            if (is_straight || is_flush)
                for (int i = 0; i < this.Cards.Length; i++)
                    this.Cards[i].InFormation = true;

            //Check value representation counts for pairs and so on.
            int[] value_representations = new int[5];
            for (int i = 0; i < 5; i++)
                value_representations[i] = this.CountValue(this.Cards[i].Value);

            //In a wheel straight, swap first and last to make the ace first.
            if (is_straight && this.Cards[0].Value == Value._2 && this.Cards[4].Value == Value.Ace)
                this.ShiftRight();

            //Precheck pair count.
            var double_count = 0;
            for (int i = 0; i < 5; i++)
                if (value_representations[i] == 2)
                    double_count++;
            var two_pairs = double_count == 4;
            var one_pair = double_count == 2;

            //Check royal flush.
            if (is_straight && this.Cards[0].Value == Value._10 && is_flush)
            {
                this.Formation = Formation.RoyalFlush;
                return true;
            }
            //Check straight flush.
            else if (is_straight && is_flush)
            {
                this.Formation = Formation.StraightFlush;
                return true;
            }
            //Check four of a kind.
            else if (value_representations[0] == 4 || value_representations[1] == 4)
            {
                this.Formation = Formation.FourOfAKind;
                var value = Value.Ace;
                //Vilka kort ingår i formationen?
                if (value_representations[0] == 4)
                    value = this.Cards[0].Value;
                else
                    value = this.Cards[1].Value;
                for (int i = 0; i < 5; i++)
                    this.Cards[i].InFormation = this.Cards[i].Value == value;
                return true;
            }
            //Check full house
            else if ((value_representations[0] == 2 && value_representations[4] == 3) || (value_representations[0] == 3 && value_representations[4] == 2))
            {
                this.Formation = Formation.FullHouse;
                for (int i = 0; i < this.Cards.Length; i++)
                    this.Cards[i].InFormation = true;
                return true;
            }
            //Check flush
            else if (is_flush)
            {
                this.Formation = Formation.Flush;
                return true;
            }
            //Check straight
            else if (is_straight)
            {
                this.Formation = Formation.Straight;
                return true;
            }
            //Check three of a kind
            else if (value_representations[0] == 3 || value_representations[2] == 3 || value_representations[4] == 3)
            {
                this.Formation = Formation.ThreeOfAKind;
                if (value_representations[0] == 3)
                {
                    this.Cards[0].InFormation = true; this.Cards[1].InFormation = true; this.Cards[2].InFormation = true;
                }
                else if (value_representations[4] == 3)
                {
                    this.Cards[2].InFormation = true; this.Cards[3].InFormation = true; this.Cards[4].InFormation = true;
                }
                else
                {
                    this.Cards[1].InFormation = true; this.Cards[2].InFormation = true; this.Cards[3].InFormation = true;
                }
                return true;
            }
            //Check two pairs
            else if (two_pairs)
            {
                this.Formation = Formation.TwoPairs;
                for (int i = 0; i < 5; i++)
                    this.Cards[i].InFormation = value_representations[i] == 2;
                return true;
            }
            //Check pair
            else if (one_pair)
            {
                this.Formation = Formation.Pair;
                for (int i = 0; i < 5; i++)
                    this.Cards[i].InFormation = value_representations[i] == 2;
                return true;
            }
            else
            {
                this.Formation = Formation.Nothing;
                this.Cards[4].InFormation = true;
                return true;
            }
        }
    }
}
