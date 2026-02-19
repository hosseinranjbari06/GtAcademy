using ErrorOr;
using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Commands.EditUserProfile
{
    public record EditUserProfileCommand(EditUserProfileDto ProfileDto) : IRequest<ErrorOr<bool>>;
}
