using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Client.Store.Github
{
    public record GithubState(string State);

    public class GithubFeatureState : Feature<GithubState>
    {
        public override string GetName() => nameof(GithubState);

        protected override GithubState GetInitialState()
        {
            return new GithubState($"{Guid.NewGuid()}a46eb-{Guid.NewGuid()}");
        }
    }
}
