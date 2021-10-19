using GithubContributionArt.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Services
{
    public interface IUserService
    {
        public Task<User> GetUserAsync(string token);
    }
}
