using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.RemoveArt
{
    public class RemoveArtMinutesChangedReducer : Reducer<RemoveArtState, RemoveArtMinutesChangedAction>
    {
        public override RemoveArtState Reduce(RemoveArtState state, RemoveArtMinutesChangedAction action)
        {
            return new RemoveArtState(action.NumMinutes);
        }
    }
}
