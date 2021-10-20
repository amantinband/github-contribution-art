using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GithubContributionArt.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly IHttpContextAccessor contextAccessor;
        private readonly ILogger<HttpContextAccessor> logger;

        public ErrorController(
            IHttpContextAccessor contextAccessor,
            ILogger<HttpContextAccessor> logger)
        {
            this.contextAccessor = contextAccessor;
            this.logger = logger;
        }

        [Route("error")]
        public IActionResult HandleError()
        {
            IExceptionHandlerFeature exceptionHandlerFeature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (exceptionHandlerFeature.Error is null)
            {
                this.logger.LogError("No exception present in the exception handler feature.");
            }
            else
            {
                this.logger.LogError(exceptionHandlerFeature.Error, "Unhandled error occurred.");
            }

            return this.Problem(
                title: "An unexpected error occurred",
                statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
