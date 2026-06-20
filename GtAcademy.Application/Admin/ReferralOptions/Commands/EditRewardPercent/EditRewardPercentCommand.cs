using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.ReferralOptions.Commands.EditRewardPercent
{
    public record EditRewardPercentCommand(float RewardPercent) : IRequest<ErrorOr<bool>>;
}
