using GithubContributionArt.Server.Models;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Services
{
    public interface IGithubArtService
    {
        public Task PushArtAsync(
            ContributionLevel[,] contributionGrid,
            User user,
            string token,
            int? removeArtMinutes);
    }
}
