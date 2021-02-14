# GenericPokerFormationChecker
A class that can identify poker hands, for .NET 5.0.

**Install version 1.0.2 from NuGet:**

```Install-Package PokerFormationChecker```

**Example code:**

```C#
using System;
using Winsoft.Gaming.GenericPokerFormationChecker;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create a formation checker. Add five cards as string.
            var fm = new FormationChecker("HRT02, SPD03, HRT04, DMN05, CLB06"); //Input. Exception if string contains errors.

            //This will give true the formation is expressed correctly.
            fm.CheckFormation();

            //What is it?
            Console.WriteLine(fm.ToString()); //Output
        }
    }
}
```

Input in example above:

`HRT02, SPD03, HRT04, DMN05, CLB06`

Output from example above:

`FORMATION=STRAIGHT,SCORE=0420,HAND=HRT02*-SPD03*-HRT04*-DMN05*-CLB06*`

Star (*) denotes that the card is included in a formation.

Sample input 2:

`CLB06, SPD03, DMN07, DMN05, HRT06`

Output from sample input 2:

`FORMATION=PAIR,SCORE=0112,HAND=SPD03-DMN05-HRT06*-CLB06*-DMN07`

Sample input 3:

`SPD08, SPD09, DMN08, DMN09, CLB08`

Output from sample input 3:

`FORMATION=FULL-HOUSE,SCORE=0642,HAND=DMN08*-CLB08*-SPD08*-DMN09*-SPD09*`

Sample input 3:

`HRTAC, SPDAC, DMNAC, DMNKN, CLBAC`

Output from sample input 3:

`FORMATION=4-OF-A-KIND,SCORE=0756,HAND=DMNKN-HRTAC*-DMNAC*-CLBAC*-SPDAC*`

Sample input 4:

`HRTAC, SPDKN, DMN10, DMNKI, CLBQU`

Output from sample input 4:

`FORMATION=STRAIGHT,SCORE=0460,HAND=DMN10*-SPDKN*-CLBQU*-DMNKI*-HRTAC*`

Sample input 5:

`DMN02, DMNKN, CLBAC, HRT05, CLB09`

Output from sample input 6:

`FORMATION=NOTHING,SCORE=0014,HAND=DMN02-HRT05-CLB09-DMNKN-CLBAC*`

Only one present star (*) denotes that no formation was found, and the hand score is equal to the score of the card with the highest score.
