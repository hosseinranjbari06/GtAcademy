using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Users.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, ErrorOr<UserProfileDto>>
    {
        private readonly IGenericService<User> _genericUserService;

        private readonly IMapper _mapper;

        public GetUserProfileQueryHandler(IMapper mapper, IGenericService<User> genericUserService)
        {
            _mapper = mapper;
            _genericUserService = genericUserService;
        }

        public async Task<ErrorOr<UserProfileDto>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _genericUserService.GetByIdAsync(request.UserId);

            if (user == null) return Error.NotFound();

            if (!user.IsActive) return Error.Conflict();

            return _mapper.Map<UserProfileDto>(user);
        }
    }
}
