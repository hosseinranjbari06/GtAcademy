using GtAcademy.Application.Users.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetTeachersListForAdmin
{
    public record GetTeachersListForAdminQuery() : IRequest<List<UserSummaryDto>>;
}
