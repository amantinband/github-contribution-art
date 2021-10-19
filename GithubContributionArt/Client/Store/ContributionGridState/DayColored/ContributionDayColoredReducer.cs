using Fluxor;
using GithubContributionArt.Shared;
using System;

namespace GithubContributionArt.Client.Store.ContributionGridState.DayColored
{
    public class ContributionDayColoredReducer : Reducer<ContributionGridState, ContributionDayColoredAction>
    {
        public override ContributionGridState Reduce(ContributionGridState state, ContributionDayColoredAction action)
        {
            var contributionGrid = state.ContributionGrid.Clone() as ContributionLevel[,];
            contributionGrid[action.DayOfWeek, action.WeekOfYear] = action.ContributionLevel;

            return new ContributionGridState(contributionGrid);
        }
    }
}
