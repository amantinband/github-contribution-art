using Newtonsoft.Json;

namespace GithubContributionArt.Server.Clients.Github.Models
{
    public record CreateRepoRequest([property:JsonProperty("name")] string RepoName);
}
