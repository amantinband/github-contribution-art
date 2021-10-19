using Newtonsoft.Json;

namespace GithubContributionArt.Server.Clients.Github.Models
{
    public record ExchangeCodeWithAccessTokenResponse(
        [JsonProperty("access_token")] string AccessToken);
}
