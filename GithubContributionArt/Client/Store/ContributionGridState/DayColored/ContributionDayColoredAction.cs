using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionGridState.DayColored
{
    public class ContributionDayColoredAction
    {
        public int DayOfWeek { get; set; }

        public int WeekOfYear { get; set; }

        public ContributionLevel ContributionLevel { get; set; }
    }
}
