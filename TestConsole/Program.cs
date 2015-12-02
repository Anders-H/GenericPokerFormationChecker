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
            Console.Write("Hand> ");
            var hand = Console.ReadLine().Trim();
            if (hand == "")
               return;
         } while (true);
      }
   }
}
