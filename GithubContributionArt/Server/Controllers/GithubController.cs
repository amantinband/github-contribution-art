using GithubContributionArt.Server.Clients.Github;
using GithubContributionArt.Server.Services;
using GithubContributionArt.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GithubController : ControllerBase
    {
        private readonly IGithubClient githubClient;
        private readonly IGithubArtService githubArtService;
        private readonly IUserService userService;
        private readonly ILogger<GithubController> logger;

        public GithubController(
            IGithubClient githubClient,
            IGithubArtService githubArtService,
            IUserService userService,
            ILogger<GithubController> logger)
        {
            this.githubClient = githubClient;
            this.githubArtService = githubArtService;
            this.userService = userService;
            this.logger = logger;
        }

        [HttpPost]
        [Route("art/submit")]
        public async Task<IActionResult> Get(SubmitGithubArtRequest submitGithubArtRequest)
        {
            this.logger.LogInformation(JsonConvert.SerializeObject(submitGithubArtRequest.ContributionGrid));

            var token = await githubClient.ExchangeCodeWithAccessTokenAsync(submitGithubArtRequest.TemporaryUserCode);
            var user = await this.userService.GetUserAsync(token);

            await this.githubArtService.PushArtAsync(
                contributionGrid: submitGithubArtRequest.ContributionGrid,
                user: user,
                token: token,
                removeArtMinutes: submitGithubArtRequest.RemoveArtMinutes);

            return this.Ok(new SubmiteGithubArtResponse(new Uri($"https://github.com/{user.Nickname}")));
        }
    }
}
