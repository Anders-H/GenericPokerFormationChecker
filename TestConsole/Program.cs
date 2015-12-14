using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Winsoft.Gaming.GenericPokerFormationChecker;

namespace TestConsole
{
    class Program
    {
        private static Random rnd { get; } = new Random();

        static void Main(string[] args)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Hand> ");
                var hand = Console.ReadLine().Trim();
                if (hand == "")
                    return;

                //Does user want to test the deck?
                if (string.Compare(hand, "test_deck", true) == 0)
                {
                    TestDeck();
                    continue;
                }

                //Does user want to test the deck manager?
                if (string.Compare(hand, "test_deck_manager", true) == 0)
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
            var hand_2_quality = rnd.Next(100);
            var hands = dm.PopHands(hand_2_quality);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Hand 1:");
            var f = new FormationChecker(hands.Item1);
            f.CheckFormation();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(f.ToString());
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Hand 2 (quality {hand_2_quality}):");
            f = new FormationChecker(hands.Item2);
            f.CheckFormation();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(f.ToString());
        }

    }
}
