namespace Winsoft.Gaming.GenericPokerFormationChecker
{
    public class HandPair
    {
        public string Hand1 { get; set; }

        public string Hand2 { get; set; }

        public HandPair(string hand1, string hand2)
        {
            Hand1 = hand1;
            Hand2 = hand2;
        }
    }
}