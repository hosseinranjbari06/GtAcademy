using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentDetailsForAdmin
{
    public class GetCourseCommentDetailsForAdminQueryHandler : IRequestHandler<GetCourseCommentDetailsForAdminQuery, ErrorOr<CourseCommentDetailsDto>>
    {
        private readonly ICourseCommentService _courseCommentService;

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public GetCourseCommentDetailsForAdminQueryHandler(ICourseCommentService courseCommentService, IMapper mapper, IUserService userService)
        {
            _courseCommentService = courseCommentService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ErrorOr<CourseCommentDetailsDto>> Handle(GetCourseCommentDetailsForAdminQuery request, CancellationToken cancellationToken)
        {
            var comment = await _courseCommentService.GetCourseCommentById(request.CommentId);

            if (comment == null) return Error.NotFound();

            var commentDto = _mapper.Map<CourseCommentDetailsDto>(comment);
            commentDto.CourseTitle = comment.Course.Title;

            var userSummary = await _userService.GetUserSummary(comment.UserId);

            if (userSummary == null) return Error.NotFound();

            commentDto.User = userSummary;

            return commentDto;
        }
    }
}
