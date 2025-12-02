using ErrorOr;
using GtAcademy.Application.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Authentication.Queries.LoginWithPhone
{
    public record LoginWithPhoneQuery(string PhoneNumber) : IRequest<ErrorOr<string>>;
}
