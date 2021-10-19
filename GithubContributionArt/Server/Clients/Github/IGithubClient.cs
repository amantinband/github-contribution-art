using GithubContributionArt.Server.Clients.Github.Models;
using GithubContributionArt.Server.Models;
using System;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Clients.Github
{
    public interface IGithubClient
    {
        public Task<string> ExchangeCodeWithAccessTokenAsync(string temporaryUserCode);

        public Task<GithubUserResponse> GetGithubUserAsync(string token);

        public Task<Contributions> GetUserContributionAsync(string nickname);

        public Task<string> GetUserEmailAsync(string token);

        public Task CreateRepoAsync(string repoName, string token);

        public Task<bool> DeleteRepoAsync(User user, string repoName, string token);
    }
}
