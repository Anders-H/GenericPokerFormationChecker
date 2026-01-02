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

    PlayedAction? actionToPlay;

    if (pokerGame.ActionAllowed(action))
    {
        if (round.WaitingForPlayer == PlayerTurn.Player1)
        {
            switch (action) {
                case Action.Bet:
                    var betAmount = PromptAmount("Bet (5, 10, 15, 20 or 25)? ");
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.Bet, betAmount);
                    break;
                case Action.Pass:
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.Pass, 0);
                    break;
                case Action.Drop:
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.Drop, 0);
                    break;
                case Action.Call:
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.Call, 0);
                    break;
                case Action.Raise:
                    var raiseAmount = PromptAmount("Bet (5, 10, 15, 20 or 25)? ");
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.Raise, raiseAmount);
                    break;
                case Action.ChangeCards:
                    var cardsChanged = PromptChangeCards(pokerGame);
                    actionToPlay = new PlayedAction(PlayerTurn.Player1, Action.ChangeCards, cardsChanged.Length);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        else
        {
            actionToPlay = pokerGame.DressAction(action);
        }
    }
    else
    {
        throw new SystemException("Poker game is broken. Unallowed action given.");
    }

    if (actionToPlay == null)
        throw new SystemException("Poker game is broken. Failed to dress action.");

    pokerGame.Play(actionToPlay);

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

static int PromptAmount(string prompt)
{
    do
    {
        Console.Write(prompt);
        var input = (Console.ReadLine() ?? "").Trim();
        
        if (int.TryParse(input, out var amount))
        {
            if (amount is 5 or 10 or 15 or 20 or 25)
                return amount;
        }
        
        Console.WriteLine("Invalid amount. Please try again.");
    } while (true);
}

static int[] PromptChangeCards(TwoPlayerFiveCardStripPoker pokerGame)
{
    Console.WriteLine("Enter the cards you want to swap, separated by comma.");
    Console.WriteLine("If you want to swap the first and the last cards, type: 1,5");
    Console.WriteLine("If you don't want to swap any cards, just press Enter.");
    Console.WriteLine();

    if (pokerGame.Player1.Hand == null)
        throw new SystemException("Poker game is broken. Player hand is null.");

    for (var i = 0; i < 5; i++)
    {
        Console.WriteLine($"Card {i + 1}: {pokerGame.Player1.Hand[i]}");
    }

    do
    {
        try
        {
            var input = (Console.ReadLine() ?? "").Trim();

            if (input == "")
                return [];

            var parts = input.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var result = parts.Select(part => int.Parse(part.Trim())).ToArray();

            if (result.Length is > 0 and < 6)
                return result;

            Console.WriteLine("Invalid input. Wrong number of cards.");
        }
        catch
        {
            Console.WriteLine("Invalid input. Parse error.");
        }
    } while (true);
}