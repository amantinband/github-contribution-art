using GithubContributionArt.Server.Models;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Clients.Git
{
    public interface IGitClient
    {
        public Repository Init(string repoName, string originRepoName, User user);

        public void AddCommit(Repository repo, User user, DateTimeOffset date);

        public void Push(Repository repo, User user, string token);

        public void DeleteRepo(Repository repo);
    }
}
