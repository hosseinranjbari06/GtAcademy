using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserAvatarNameById
{
    public class GetUserAvatarNameByIdQueryHandler : IRequestHandler<GetUserAvatarNameByIdQuery, ErrorOr<string>>
    {
        public readonly IGenericService<User> _genericUserService;

        public GetUserAvatarNameByIdQueryHandler(IGenericService<User> genericUserService)
        {
            _genericUserService = genericUserService;
        }

        public async Task<ErrorOr<string>> Handle(GetUserAvatarNameByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _genericUserService.GetByIdAsync(request.UserId);

            if (user == null) return Error.NotFound();

            return user.AvatarName;
        }
    }
}
