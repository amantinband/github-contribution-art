using GithubContributionArt.Server.Clients.Git;
using GithubContributionArt.Server.Clients.Github;
using GithubContributionArt.Server.Services;
using GithubContributionArt.Shared;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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

        public GithubController(
            IGithubClient githubClient,
            IGithubArtService githubArtService,
            IUserService userService)
        {
            this.githubClient = githubClient;
            this.githubArtService = githubArtService;
            this.userService = userService;
        }

        [HttpPost]
        [Route("art/submit")]
        public async Task<IActionResult> Get(SubmitGithubArtRequest submitGithubArtRequest)
        {
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
