using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.RegisterWithPhone
{
    public record RegisterWithPhoneCommand(RegisterWithPhoneDto RegisterDto) : IRequest<ErrorOr<string>>;
}
