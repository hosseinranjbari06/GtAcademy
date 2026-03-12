using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin
{
    public record GetUsersListForAdminQuery(SearchUsersListDto SearchDto) : IRequest<List<UserListItemDto>>;
}
