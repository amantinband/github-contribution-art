using System.ComponentModel.DataAnnotations;

namespace GithubContributionArt.Server.Clients.Github
{
    public record GithubConfig
    {
        [Required]
        public string ClientId { get; init; }

        [Required]
        public string ClientSecret { get; init; }
    }
}
