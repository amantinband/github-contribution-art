using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Clients.Github.Models
{
    public record GithubUserResponse(string Name, string Email, string Login);
}
