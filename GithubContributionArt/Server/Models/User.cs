using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Models
{
    public record User(string Nickname, string FullName, string Email, Contributions CurrentContributions);
}
