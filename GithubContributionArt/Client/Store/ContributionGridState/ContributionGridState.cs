using Fluxor;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionGridState
{
    public record ContributionGridState(ContributionLevel[,] ContributionGrid);

    public class ContributionGridFeatureState : Feature<ContributionGridState>
    {
        public override string GetName() => nameof(ContributionGridState);

        protected override ContributionGridState GetInitialState()
        {
            return new ContributionGridState(new ContributionLevel[CalendarConstants.DaysInWeek, CalendarConstants.WeeksInYear]);
        }
    }
}
