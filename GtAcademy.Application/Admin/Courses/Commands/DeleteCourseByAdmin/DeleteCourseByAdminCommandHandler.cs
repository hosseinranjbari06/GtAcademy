using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.DeleteCourseByAdmin
{
    public class DeleteCourseByAdminCommandHandler : IRequestHandler<DeleteCourseByAdminCommand, ErrorOr<bool>>
    {
        public Task<ErrorOr<bool>> Handle(DeleteCourseByAdminCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
