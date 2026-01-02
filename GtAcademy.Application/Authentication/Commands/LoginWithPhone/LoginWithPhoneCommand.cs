using ErrorOr;
using GtAcademy.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.LoginWithPhone
{
    public record LoginWithPhoneCommand(LoginWithPhoneDto LoginDto) : IRequest<ErrorOr<string>>;
}
