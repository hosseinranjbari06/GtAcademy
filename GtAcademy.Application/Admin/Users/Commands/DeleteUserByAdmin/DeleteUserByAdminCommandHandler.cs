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

        public DeleteUserByAdminCommandHandler(IGenericService<User> genericUserService)
        {
            _genericUserService = genericUserService;
        }

        public Task<ErrorOr<bool>> Handle(DeleteUserByAdminCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
