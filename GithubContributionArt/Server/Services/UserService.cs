using GithubContributionArt.Server.Clients.Github;
using GithubContributionArt.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IGithubClient githubClient;

        public UserService(IGithubClient githubClient)
        {
            this.githubClient = githubClient;
        }

        public async Task<User> GetUserAsync(string token)
        {
            var githubUser = await this.githubClient.GetGithubUserAsync(token);

            if (githubUser.Email is null)
            {
                string email = await this.githubClient.GetUserEmailAsync(token);
                githubUser = githubUser with { Email = email, Name = "John Doe" };
            }

            var contributions = await this.githubClient.GetUserContributionAsync(githubUser.Login);

            return new User(
                Nickname: githubUser.Login,
                FullName: githubUser.Name,
                Email: githubUser.Email,
                CurrentContributions: contributions);
        }
    }
}
