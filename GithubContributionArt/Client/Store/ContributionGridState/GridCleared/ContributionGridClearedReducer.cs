using Fluxor;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionGridState.GridCleared
{
    public class ContributionGridClearedReducer : Reducer<ContributionGridState, ContributionGridClearedAction>
    {
        public override ContributionGridState Reduce(ContributionGridState state, ContributionGridClearedAction action)
        {
            return new ContributionGridState(new ContributionLevel[CalendarConstants.DaysInWeek, CalendarConstants.WeeksInYear]);
        }
    }
}
