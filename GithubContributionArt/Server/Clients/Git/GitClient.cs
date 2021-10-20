using GithubContributionArt.Server.Models;
using LibGit2Sharp;
using System;
using System.Collections.Generic;
using System.IO;

namespace GithubContributionArt.Server.Clients.Git
{
    public class GitClient : IGitClient
    {
        public Repository Init(string repoName, string originRepoName, User user)
        {
            var repoRelaivePath = Path.Combine(@".\ArtRepos", repoName);
            var repo = new Repository(Repository.Init(repoRelaivePath));

            repo.Network.Remotes.Update("origin", r => { r.Url = $"https://github.com/{user.Nickname}/{originRepoName}.git"; });

            var filesToCopy = new List<string> { "icon.svg", "README.md" };

            filesToCopy.ForEach(fileName =>
            {
                var sourceRelativePath = Path.Combine(@".\ArtRepos", fileName);
                var destRelativePath = Path.Combine(repoRelaivePath, fileName);

                File.Copy(sourceRelativePath, destRelativePath, overwrite: true);

                repo.Index.Add(fileName);
            });


            repo.Index.Write();
            return repo;
        }

        public void AddCommit(Repository repo, User user, DateTimeOffset date)
        {
            var author = new Signature(user.FullName, user.Email, date);
            repo.Commit("Pixel", author, author, new CommitOptions { AllowEmptyCommit = true });
        }
        public void Push(Repository repo, User user, string token)
        {
            var pushOptions = new PushOptions
            {
                CredentialsProvider = (_, _, _) => new UsernamePasswordCredentials { Username = user.Nickname, Password = token }
            };

            repo.Network.Push(repo.Network.Remotes["origin"], "+refs/heads/master", pushOptions);
        }

        public void DeleteRepo(Repository repo)
        {
            var path = repo.Info.WorkingDirectory;
            repo.Dispose();
            this.DeleteDirRecursively(path);
        }

        private void DeleteDirRecursively(string path)
        {
            DirectoryInfo di = new(path);

            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                DeleteDirRecursively(dir.FullName);
            }
            foreach (FileInfo file in di.GetFiles())
            {
                file.Attributes = FileAttributes.Normal;
                file.Delete();
            }

            Directory.Delete(path);
        }
    }
}
