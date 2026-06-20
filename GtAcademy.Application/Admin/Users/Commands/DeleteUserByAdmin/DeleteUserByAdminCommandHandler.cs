using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Users.Commands.DeleteUserByAdmin
{
    public class DeleteUserByAdminCommandHandler : IRequestHandler<DeleteUserByAdminCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<User> _genericUserService;

        private readonly IUserService _userService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserByAdminCommandHandler(IGenericService<User> genericUserService, IUserService userService, IUnitOfWork unitOfWork)
        {
            _genericUserService = genericUserService;
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteUserByAdminCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserWithRelations(request.UserId);

            if (user == null) return Error.NotFound();

            user.IsDeleted = true;
            user.IsActive = false;
            user.Roles.Clear();

            _genericUserService.Update(user);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
