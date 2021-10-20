using GithubContributionArt.Server.Clients.Git;
using GithubContributionArt.Server.Clients.Github;
using GithubContributionArt.Server.Models;
using GithubContributionArt.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Services
{
    public class GithubArtService : IGithubArtService
    {
        private readonly IGitClient gitClient;
        private readonly IGithubClient githubClient;

        public GithubArtService(
            IGithubClient githubClient,
            IGitClient gitClient)
        {
            this.gitClient = gitClient;
            this.githubClient = githubClient;
        }
        public async Task PushArtAsync(ContributionLevel[,] contributionGrid, User user, string token, int? removeArtMinutes)
        {
            if (await this.githubClient.DeleteRepoAsync(user, RepoConstants.RepoName, token))
            {
                await Task.Delay(3000);
            }

            await this.githubClient.CreateRepoAsync(RepoConstants.RepoName, token);

            var repo = this.gitClient.Init(repoName: user.Nickname, originRepoName: RepoConstants.RepoName, user);
            try
            {

                var multiplier = (int)(user.CurrentContributions.MaxDayContributions <= 8 ? 1 : Math.Ceiling(user.CurrentContributions.MaxDayContributions / 8.0));

                Dictionary<ContributionLevel, int> contributionLevelToNumCommitsMap = new()
                {
                    [ContributionLevel.L0] = 0,
                    [ContributionLevel.L1] = 1,
                    [ContributionLevel.L2] = 2 * multiplier,
                    [ContributionLevel.L3] = 3 * multiplier,
                    [ContributionLevel.L4] = 4 * multiplier,
                };

                for (var day = 0; day < contributionGrid.GetLength(0); day++)
                {
                    for (var week = 0; week < contributionGrid.GetLength(1); week++)
                    {
                        var numCommits = contributionLevelToNumCommitsMap[contributionGrid[day, week]] - user.CurrentContributions.ContributionGrid[day, week];

                        for (var i = 0; i < numCommits; i++)
                        {
                            this.gitClient.AddCommit(repo, user, user.CurrentContributions.FirstSundayInGrid.AddDays(day + week * 7));
                        }
                    }
                }

                this.gitClient.Push(repo, user, token);

                if (removeArtMinutes.HasValue)
                {
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay((int)TimeSpan.FromMinutes(removeArtMinutes.Value).TotalMilliseconds);
                        await this.githubClient.DeleteRepoAsync(user, RepoConstants.RepoName, token);
                    });
                }
                this.gitClient.DeleteRepo(repo);
            }
            catch
            {
                this.gitClient.DeleteRepo(repo);
                throw;
            }

        }
    }
}
