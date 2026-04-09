using GtAcademy.Domain.Courses;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface ICourseCategoryService
    {
        Task<bool> IsCategoryTitleExist(string title);

        Task<CourseCategory?> GetCourseCategoryForEditById(Guid categoryId);
    }
}
