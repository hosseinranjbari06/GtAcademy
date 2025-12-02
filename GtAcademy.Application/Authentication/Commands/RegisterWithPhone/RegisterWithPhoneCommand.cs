using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.RegisterWithPhone
{
    public record RegisterWithPhoneCommand(string UserName, string PhoneNumber) : IRequest<ErrorOr<string>>;
}
