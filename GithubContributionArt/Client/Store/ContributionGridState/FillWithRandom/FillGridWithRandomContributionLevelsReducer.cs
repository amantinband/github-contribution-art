using Fluxor;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionGridState.FillWithRandom
{
    public class FillGridWithRandomContributionLevelsReducer : Reducer<ContributionGridState, FillGridWithRandomContributionLevelsAction>
    {
        public override ContributionGridState Reduce(ContributionGridState state, FillGridWithRandomContributionLevelsAction action)
        {
            var contributionGrid = new ContributionLevel[CalendarConstants.DaysInWeek, CalendarConstants.WeeksInYear];
            var contributionValues = Enum.GetValues(typeof(ContributionLevel));
            var random = new Random();

            for (var day = 0; day < CalendarConstants.DaysInWeek; day++)
            {
                for (var week = 0; week < CalendarConstants.WeeksInYear; week++)
                {
                    contributionGrid[day, week] = (ContributionLevel)contributionValues.GetValue(random.Next(contributionValues.Length));
                }
            }

            return new ContributionGridState(contributionGrid);
        }
    }
}
