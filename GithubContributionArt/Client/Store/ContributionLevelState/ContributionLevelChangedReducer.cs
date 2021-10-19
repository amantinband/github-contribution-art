using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionLevelState
{
    public class ContributionLevelChangedReducer : Reducer<ContributionLevelState, ContributionLevelChangedAction>
    {
        public override ContributionLevelState Reduce(ContributionLevelState state, ContributionLevelChangedAction action)
        {
            return state with
            {
                ContributionLevel = action.NewContributionLevel,
            };
        }
    }
}
