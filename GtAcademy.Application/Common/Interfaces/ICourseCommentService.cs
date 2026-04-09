using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentDetailsForAdmin;
using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin;
using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface ICourseCommentService
    {
        Task<SearchCourseCommentsListDto> GetCourseCommentsListForAdmin(SearchCourseCommentsListDto searchDto);

        Task<int> GetCommentsCount();

        Task<CourseComment?> GetCourseCommentById(Guid commentId);
    }
}
