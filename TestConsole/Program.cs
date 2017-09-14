using System;
using Winsoft.Gaming.GenericPokerFormationChecker;
using static System.String;

namespace TestConsole
{
    public class Program
    {
        private static Random Rnd { get; } = new Random();
        public static void Main()
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Hand> ");
                var readLine = Console.ReadLine();
                if (readLine == null)
                    continue;
                var hand = readLine.Trim();
                if (hand == "")
                    return;

                //Does user want to test the deck?
                if (Compare(hand, "test_deck", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    TestDeck();
                    continue;
                }

                //Does user want to test the deck manager?
                if (Compare(hand, "test_deck_manager", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    TestDeckManager();
                    continue;
                }

                //Else, assume user has entered a hand.
                try
                {
                    var f = new FormationChecker(hand);
                    if (f.CheckFormation())
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(f.ToString());
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Can't check now.");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.GetType());
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(ex.Message);
                }
            } while (true);
        }
        public static void TestDeck()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            var d = new Deck();
            d.Shuffle();
            for (var i = 0; i < 52; i += 4)
                Console.WriteLine($"{i + 1}: {d.Pop()}   {i + 2}: {d.Pop()}   {i + 3}: {d.Pop()}   {i + 4}: {d.Pop()}");
        }
        public static void TestDeckManager()
        {
            var dm = new DeckManager();
            var hand2Quality = Rnd.Next(100);
            var hands = dm.PopHands(hand2Quality);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hand 1:");
            var f = new FormationChecker(hands.hand1);
            f.CheckFormation();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(f.ToString());
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Hand 2 (quality {hand2Quality}):");
            f = new FormationChecker(hands.hand2);
            f.CheckFormation();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(f.ToString());
        }
    }
}
