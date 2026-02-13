using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUsersCount
{
    public class GetUsersCountQueryHandler : IRequestHandler<GetUsersCountQuery, int>
    {
        private readonly IGenericService<User> _genericUserService;

        public GetUsersCountQueryHandler(IGenericService<User> genericUserService)
        {
            _genericUserService = genericUserService;
        }

        public async Task<int> Handle(GetUsersCountQuery request, CancellationToken cancellationToken)
        {
            return await _genericUserService.GetCountAsync();
        }
    }
}
