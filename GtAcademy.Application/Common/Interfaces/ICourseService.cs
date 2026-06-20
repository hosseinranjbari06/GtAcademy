using GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseSummaryDto>> GetCoursesList(string search = "", string categoryTitle = "", int seperate = 6, int pageId = 1);

        Task<List<CourseSummaryDto>> GetPopularCoursesList(int take);

        Task<Course?> GetCourseWithEpisodes(Guid courseId);

        Task<bool> IsCourseExist(Guid courseId);

        Task<List<CourseCommentDto>> GetCourseCommentDtos(Guid courseId);

        Task<int> GetCourseStudentsCount(Guid courseId);

        Task<int> GetAllStudentsCount();

        Task<List<CourseCategoryDto>> GetCourseCategories();

        #region Admin

        Task<List<CourseListItemDto>> GetCoursesListForAdmin(SearchCourseListDto searchDto);

        Task<Course?> GetCourseWithRelations(Guid courseId);

        #endregion
    }
}
