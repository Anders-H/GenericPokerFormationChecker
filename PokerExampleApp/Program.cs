using Winsoft.Gaming.GenericPokerFormationChecker.GameState;
using Winsoft.Gaming.GenericPokerFormationChecker.GameState.GameStyles;
using Action = Winsoft.Gaming.GenericPokerFormationChecker.GameState.Action;

var humanPlayer = new TwoPlayerFiveCardStripPokerPlayer("You", false, "sweater", "socks", "pants", "underpants");
var computerPlayer = new TwoPlayerFiveCardStripPokerPlayer("Maria", false, "skirt", "socks", "blouse", "panties");
var pokerGame = new TwoPlayerFiveCardStripPoker(humanPlayer, computerPlayer);

do
{
    var round = pokerGame.NextRound();
    var actions = round.GetAllowedActions();
    Action action;

    if (round.WaitingForPlayer == PlayerTurn.Player1)
    {
        Console.WriteLine(pokerGame.ToString(actions));
        var answer = GetAction(actions.Count);

        if (answer == 0)
        {
            Console.WriteLine("Quit? (y/n).");
            var input = (Console.ReadLine() ?? "").Trim().ToLower();

            if (input.StartsWith("y"))
                return 0;
        }

        action = actions[answer - 1];
    }
    else
    {
        Console.WriteLine("Computer is thinking...");
        action = pokerGame.GetComputerAction();
    }

    PlayedAction? playedAction;

    if (pokerGame.ActionAllowed(action))
    {
        if (round.WaitingForPlayer == PlayerTurn.Player1)
        {
            switch (action) {
                case Action.Bet:
                    break;
                case Action.Pass:
                    break;
                case Action.Drop:
                    break;
                case Action.Call:
                    break;
                case Action.Raise:
                    break;
                case Action.ChangeCards:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            playedAction = pokerGame.DressAction(action);
        }
    }
    else
    {
        throw new SystemException("Poker game is broken. Unallowed action given.");
    }

    if (playedAction == null)
        throw new SystemException("Poker game is broken. Failed to dress action.");

} while (true);

static int GetAction(int count)
{
    if (count <= 0)
    {
        do
        {
            Console.Write(">");
            var input = (Console.ReadLine() ?? "").Trim();

            if (int.TryParse(input, out var actionNumber))
            {
                if (actionNumber == 0)
                    return actionNumber;
            }

            Console.WriteLine("Invalid action number. Please try again. Type 0.");
        } while (true);
    }

    do
    {
        Console.Write(">");
        var input = (Console.ReadLine() ?? "").Trim();

        if (int.TryParse(input, out var actionNumber))
        {
            if (actionNumber >= 0 && actionNumber <= count)
                return actionNumber;
        }

        Console.WriteLine($"Invalid action number. Please try again. Type 1 to {count}, or 0.");
    } while (true);
}