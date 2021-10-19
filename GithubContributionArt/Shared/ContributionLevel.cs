using System.Text.Json.Serialization;

namespace GithubContributionArt.Shared
{
    /// <summary>
    /// Contribution level in ascending order.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum ContributionLevel
    {
        L0,
        L1,
        L2,
        L3,
        L4,
    }
}
