using Fluxor;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionLevelState
{
    public record ContributionLevelState(ContributionLevel ContributionLevel);

    public class ContributionLevelFeatureState : Feature<ContributionLevelState>
    {
        public override string GetName() => nameof(ContributionLevelFeatureState);

        protected override ContributionLevelState GetInitialState()
        {
            return new ContributionLevelState(ContributionLevel.L4);
        }
    }
}
