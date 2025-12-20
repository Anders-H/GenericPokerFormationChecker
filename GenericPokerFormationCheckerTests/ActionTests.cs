using Winsoft.Gaming.GenericPokerFormationChecker.GameState;
using Xunit;

namespace GenericPokerFormationCheckerTests;

public class ActionTests
{
    [Fact]
    public void EnumHas()
    {
        var items = ActionHelper.GetAll();
        Assert.Equal(6, items.Count);
        Assert.Contains(Action.Bet, items);
        Assert.Contains(Action.Pass, items);
        Assert.Contains(Action.Drop, items);
        Assert.Contains(Action.Call, items);
        Assert.Contains(Action.Raise, items);
        Assert.Contains(Action.ChangeCards, items);
    }

    [Fact]
    public void IsAllowed()
    {
        var actionList = new ActionList
        {
            Action.Bet,
            Action.Raise
        };

        Assert.True(actionList.IsAllowed(Action.Bet));
        Assert.True(actionList.IsAllowed(Action.Raise));
        Assert.False(actionList.IsAllowed(Action.Pass));
        Assert.False(actionList.IsAllowed(Action.Drop));
        Assert.False(actionList.IsAllowed(Action.Call));
        Assert.False(actionList.IsAllowed(Action.ChangeCards));

        actionList.AddAllowedAction(Action.Bet);

        Assert.True(actionList.IsAllowed(Action.Bet));
        Assert.True(actionList.IsAllowed(Action.Raise));
        Assert.False(actionList.IsAllowed(Action.Pass));
        Assert.False(actionList.IsAllowed(Action.Drop));
        Assert.False(actionList.IsAllowed(Action.Call));
        Assert.False(actionList.IsAllowed(Action.ChangeCards));

        actionList.AddAllowedAction(Action.Drop);

        Assert.True(actionList.IsAllowed(Action.Bet));
        Assert.True(actionList.IsAllowed(Action.Raise));
        Assert.False(actionList.IsAllowed(Action.Pass));
        Assert.True(actionList.IsAllowed(Action.Drop));
        Assert.False(actionList.IsAllowed(Action.Call));
        Assert.False(actionList.IsAllowed(Action.ChangeCards));

        Assert.True(actionList.AreOnlyAllowed(Action.Bet, Action.Raise, Action.Drop));
        Assert.False(actionList.AreOnlyAllowed(Action.Bet, Action.Drop));
        Assert.False(actionList.AreOnlyAllowed(Action.Call, Action.Raise, Action.Drop));
    }
}