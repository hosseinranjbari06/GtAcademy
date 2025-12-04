using GtAcademy.Application.Courses.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface ICourseService
    {
        Task<List<CourseSummaryDto>> GetCoursesList(string search = "", int seperate = 6, int pageId = 1);
    }
}
