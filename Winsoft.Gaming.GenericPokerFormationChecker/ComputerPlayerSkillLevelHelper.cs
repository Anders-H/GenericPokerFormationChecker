namespace Winsoft.Gaming.GenericPokerFormationChecker;

public static class ComputerPlayerSkillLevelHelper
{
    public static int FromLevelToQuality(ComputerPlayerSkillLevel level) =>
        level switch
        {
            ComputerPlayerSkillLevel.Useless => 1,
            ComputerPlayerSkillLevel.Mediocre => 15,
            ComputerPlayerSkillLevel.Good => 30,
            ComputerPlayerSkillLevel.Expert => 90,
            _ => 1
        };
}