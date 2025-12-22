using Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;

var humanPlayer = new TwoPlayerFiveCardStripPokerPlayer("You", false, "sweater", "socks", "pants", "underpants");
var computerPlayer = new TwoPlayerFiveCardStripPokerPlayer("Maria", false, "skirt", "socks", "blouse", "panties");
var pokerGame = new TwoPlayerFiveCardStripPoker(humanPlayer, computerPlayer);

do
{
    var round = pokerGame.NextRound();
    Console.WriteLine(pokerGame.ToString());
    var answer = (Console.ReadLine() ?? "").Trim();

} while (true);