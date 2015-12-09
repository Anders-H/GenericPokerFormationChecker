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
        static void Main(string[] args)
        {
            do
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("Hand> ");
                var hand = Console.ReadLine().Trim();
                if (hand == "")
                    return;
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
    }
}
