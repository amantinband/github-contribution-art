using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubContributionArt.Shared.Validators
{
    class SubmitGithubArtRequestValidator : AbstractValidator<SubmitGithubArtRequest>
    {
        public SubmitGithubArtRequestValidator()
        {
            RuleFor(x => x.ContributionGrid.GetLength(0) == CalendarConstants.DaysInWeek);
            RuleFor(x => x.ContributionGrid.GetLength(1) == CalendarConstants.WeeksInYear);
            RuleFor(x => x.ContributionGrid).Must(x =>
            {
                for (var i = 0; i < CalendarConstants.DaysInWeek; i++)
                {
                    for (var j = 0; j < CalendarConstants.WeeksInYear; j++)
                    {
                        if (!Enum.IsDefined(x[i, j]))
                        {
                            return false;
                        }
                    }
                }
                return true;
            });

            RuleFor(x => x.RemoveArtMinutes).GreaterThan(0).LessThanOrEqualTo(30);
            RuleFor(x => x.TemporaryUserCode).NotEmpty().NotNull();
        }
    }
}
