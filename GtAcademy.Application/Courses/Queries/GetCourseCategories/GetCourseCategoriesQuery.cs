using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCourseCategories
{
    public record GetCourseCategoriesQuery() : IRequest<List<CourseCategoryDto>>;
}
