using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoriesListForAdmin
{
    public record GetCourseCategoriesListForAdminQuery() : IRequest<List<CourseCategoryDto>>;
}
