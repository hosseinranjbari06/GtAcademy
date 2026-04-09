using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin
{
    public class GetCourseCommentsListForAdminQueryHandler : IRequestHandler<GetCourseCommentsListForAdminQuery, SearchCourseCommentsListDto>
    {
        private readonly ICourseCommentService _courseCommentService;

        public GetCourseCommentsListForAdminQueryHandler(ICourseCommentService courseCommentService)
        {
            _courseCommentService = courseCommentService;
        }

        public async Task<SearchCourseCommentsListDto> Handle(GetCourseCommentsListForAdminQuery request, CancellationToken cancellationToken)
        {
            return await _courseCommentService.GetCourseCommentsListForAdmin(request.SearchDto);
        }
    }
}
