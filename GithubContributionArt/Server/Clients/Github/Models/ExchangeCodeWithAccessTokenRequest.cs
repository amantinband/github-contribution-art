using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Clients.Github.Models
{
    public record ExchangeCodeWithAccessTokenRequest(
        [property: JsonProperty("client_id")] string ClientId,
        [property: JsonProperty("client_secret")] string ClientSecret,
        string Code);
}
