using GithubContributionArt.Server.Clients.Github.Models;
using GithubContributionArt.Server.Models;
using GithubContributionArt.Shared;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Clients.Github
{
    public class GithubClient : IGithubClient
    {
        private const string BaseUrl = "https://api.github.com";
        private HttpClient httpClient;
        private GithubConfig githubConfig;

        public GithubClient(
            IHttpClientFactory httpClientFactory,
            IOptions<GithubConfig> githubConfig)
        {
            this.httpClient = httpClientFactory.CreateClient();
            this.githubConfig = githubConfig.Value;
        }

        public async Task CreateRepoAsync(string repoName, string token)
        {
            var content = new CreateRepoRequest(repoName);

            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri("https://api.github.com/user/repos"),
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json")
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            httpRequestMessage.Headers.Add("user-agent", ".net");

            var response = await this.httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteRepoAsync(User user, string repoName, string token)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://api.github.com/repos/{user.Nickname}/{repoName}"),
                Method = HttpMethod.Delete,
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            httpRequestMessage.Headers.Add("user-agent", ".net");

            var response = await this.httpClient.SendAsync(httpRequestMessage);

            return response.IsSuccessStatusCode;
        }

        public async Task<string> ExchangeCodeWithAccessTokenAsync(string temporaryUserCode)
        {
            using var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri("https://github.com/login/oauth/access_token"),
                Method = HttpMethod.Post,
            };

            var content = new ExchangeCodeWithAccessTokenRequest(
                ClientId: this.githubConfig.ClientId,
                ClientSecret: this.githubConfig.ClientSecret,
                Code: temporaryUserCode);

            httpRequestMessage.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            var response = await httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<ExchangeCodeWithAccessTokenResponse>(await response.Content.ReadAsStringAsync()).AccessToken;
        }

        public async Task<string> GetUserEmailAsync(string token)
        {
            using var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseUrl}/user/emails"),
                Method = HttpMethod.Get,
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            httpRequestMessage.Headers.Add("user-agent", ".net");

            var response = await httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();


            return JsonConvert.DeserializeObject<EmailResponse[]>(await response.Content.ReadAsStringAsync())
                .First()
                .Email;
        }

        public async Task<Contributions> GetUserContributionAsync(string nickname)
        {
            var contributionPage = await this.httpClient.GetStringAsync($"https://github.com/users/{nickname}/contributions");
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(contributionPage);

            var nodes = htmlDoc.DocumentNode.Descendants(0).Where(n => n.HasClass("ContributionCalendar-day"));
            var firstSundayInGrid = Convert.ToDateTime(nodes.First().Attributes["data-date"].Value);

            int[,] contributionGrid = new int[CalendarConstants.DaysInWeek, CalendarConstants.WeeksInYear];

            var maxDayContributions = nodes
                .Where(n => n.Attributes.Contains("data-count"))
                .Select((n, i) => {
                    var numContributions = Int32.Parse(n.Attributes["data-count"].Value);
                    contributionGrid[i % CalendarConstants.DaysInWeek, i / CalendarConstants.DaysInWeek] = numContributions;
                    return numContributions;
                })
                .Max();

            return new Contributions(
                contributionGrid,
                maxDayContributions,
                firstSundayInGrid);
        }

        public async Task<GithubUserResponse> GetGithubUserAsync(string token)
        {
            using var httpRequestMessage = new HttpRequestMessage
            {
                RequestUri = new Uri($"{BaseUrl}/user"),
                Method = HttpMethod.Get,
            };

            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("token", token);
            httpRequestMessage.Headers.Add("user-agent", ".net");

            var response = await httpClient.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();


            return JsonConvert.DeserializeObject<GithubUserResponse>(await response.Content.ReadAsStringAsync());
        }
    }
}
