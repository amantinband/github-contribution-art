using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Models
{
    public record Contributions(
        int[,] ContributionGrid,
        int MaxDayContributions,
        DateTimeOffset FirstSundayInGrid);
}
