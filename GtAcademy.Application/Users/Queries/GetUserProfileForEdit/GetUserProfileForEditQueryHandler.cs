using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using GtAcademy.Application.Users.Queries.GetUserProfile;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfileForEdit
{
    public class GetUserProfileForEditQueryHandler : IRequestHandler<GetUserProfileForEditQuery, ErrorOr<EditUserProfileDto>>
    {
        private readonly IGenericService<User> _genericUserService;

        private readonly IMapper _mapper;

        public GetUserProfileForEditQueryHandler(IMapper mapper, IGenericService<User> genericUserService)
        {
            _mapper = mapper;
            _genericUserService = genericUserService;
        }

        public async Task<ErrorOr<EditUserProfileDto>> Handle(GetUserProfileForEditQuery request, CancellationToken cancellationToken)
        {
            var user = await _genericUserService.GetByIdAsync(request.UserId);

            if (user == null) return Error.NotFound();

            if (!user.IsActive) return Error.Conflict();

            return _mapper.Map<EditUserProfileDto>(user);
        }
    }
}
