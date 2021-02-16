using Winsoft.Gaming.GenericPokerFormationChecker;
using Xunit;

namespace GenericPokerFormationCheckerTests
{
    public class FormationDescriptionTests
    {
        [Theory]
        [InlineData("SPD02,DMNAC,CLB04,HRT09,HRT06", "FORMATION=NOTHING,SCORE=0014,HAND=SPD02-CLB04-HRT06-HRT09-DMNAC*")]
        [InlineData("SPD02,DMN02,CLB04,HRT09,HRT06", "FORMATION=PAIR,SCORE=0104,HAND=DMN02*-SPD02*-CLB04-HRT06-HRT09")]
        [InlineData("SPD02,DMN02,CLB04,HRT04,HRT06", "FORMATION=2-PAIRS,SCORE=0212,HAND=DMN02*-SPD02*-HRT04*-CLB04*-HRT06")]
        [InlineData("SPD02,DMN02,CLB02,HRT09,HRT06", "FORMATION=3-OF-A-KIND,SCORE=0306,HAND=DMN02*-CLB02*-SPD02*-HRT06-HRT09")]
        [InlineData("SPD02,DMNAC,CLB05,HRT03,HRT04", "FORMATION=STRAIGHT,SCORE=0428,HAND=DMNAC*-SPD02*-HRT03*-HRT04*-CLB05*")]
        [InlineData("SPD02,SPD10,SPD05,SPD09,SPD04", "FORMATION=FLUSH,SCORE=0530,HAND=SPD02*-SPD04*-SPD05*-SPD09*-SPD10*")]
        [InlineData("SPD02,HRT02,SPD03,HRT03,CLB02", "FORMATION=FULL-HOUSE,SCORE=0612,HAND=HRT02*-CLB02*-SPD02*-HRT03*-SPD03*")]
        [InlineData("HRT02,HRT10,SPD02,CLB02,DMN02", "FORMATION=4-OF-A-KIND,SCORE=0708,HAND=HRT02*-DMN02*-CLB02*-SPD02*-HRT10")]
        [InlineData("SPD02,SPDAC,SPD05,SPD03,SPD04", "FORMATION=STRAIGHT-FLUSH,SCORE=0828,HAND=SPDAC*-SPD02*-SPD03*-SPD04*-SPD05*")]
        [InlineData("CLBKN,CLB10,CLBKI,CLBQU,CLBAC", "FORMATION=ROYAL-FLUSH,SCORE=0960,HAND=CLB10*-CLBKN*-CLBQU*-CLBKI*-CLBAC*")]
        public void CanDescribeFormation(string source, string target)
        {
            var formationChecker = new FormationChecker(source);
            formationChecker.CheckFormation();
            Assert.True(formationChecker.ToString() == target);
        }
    }
}