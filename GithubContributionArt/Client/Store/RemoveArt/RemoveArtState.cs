using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.RemoveArt
{
    public record RemoveArtState(short? RemoveArtMinutes);

    public class RemoveArtFeatureState : Feature<RemoveArtState>
    {
        public override string GetName() => nameof(RemoveArtFeatureState);

        protected override RemoveArtState GetInitialState()
        {
            return new RemoveArtState(5);
        }
    }
}
