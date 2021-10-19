using Fluxor;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.ContributionGridState.SetGrid
{
    public class SetGridReducer : Reducer<ContributionGridState, SetGridAction>
    {
        public override ContributionGridState Reduce(ContributionGridState state, SetGridAction action)
        {
            return new ContributionGridState(action.ContributionGrid);
        }
    }
}
