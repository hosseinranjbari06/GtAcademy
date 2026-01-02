using ErrorOr;
using GtAcademy.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Commands.VerifyPhoneNumber
{
    public record VerifyPhoneNumberCommand(VerifyPhoneNumberDto VerifyDto) : IRequest<ErrorOr<AuthenticationResult>>;
}
