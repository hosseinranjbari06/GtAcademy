using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Commands.DeleteCourseCommentByAdmin
{
    public class DeleteCourseCommentByAdminCommandHandler : IRequestHandler<DeleteCourseCommentByAdminCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<CourseComment> _genericCommentService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCommentByAdminCommandHandler(IGenericService<CourseComment> genericCommentService, IUnitOfWork unitOfWork)
        {
            _genericCommentService = genericCommentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteCourseCommentByAdminCommand request, CancellationToken cancellationToken)
        {
            var comment = await _genericCommentService.GetByIdAsync(request.CommentId);

            if (comment == null) return Error.NotFound();

            _genericCommentService.Delete(comment);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
