namespace GithubContributionArt.Shared
{
    public record SubmitGithubArtRequest(
        ContributionLevel[,] ContributionGrid,
        string TemporaryUserCode,
        int? RemoveArtMinutes);
}
