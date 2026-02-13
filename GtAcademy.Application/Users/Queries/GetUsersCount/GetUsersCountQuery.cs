using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUsersCount
{
    public record GetUsersCountQuery() : IRequest<int>;
}
