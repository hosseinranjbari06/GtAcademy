using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Commands.SubmitCourseCommentByAdmin
{
    public class SubmitCourseCommentByAdminCommandHandler : IRequestHandler<SubmitCourseCommentByAdminCommand, ErrorOr<bool>>
    {
        private readonly ICourseCommentService _courseCommentService;

        private readonly IGenericService<CourseComment> _genericCommentService;

        private readonly IUnitOfWork _unitOfWork;

        public SubmitCourseCommentByAdminCommandHandler(ICourseCommentService courseCommentService, IGenericService<CourseComment> genericCommentService, IUnitOfWork unitOfWork)
        {
            _courseCommentService = courseCommentService;
            _genericCommentService = genericCommentService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<bool>> Handle(SubmitCourseCommentByAdminCommand request, CancellationToken cancellationToken)
        {
            var comment = await _courseCommentService.GetCourseCommentById(request.CommentId);

            if (comment == null) return Error.NotFound();

            comment.AdminSubmited = true;

            _genericCommentService.Update(comment);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
