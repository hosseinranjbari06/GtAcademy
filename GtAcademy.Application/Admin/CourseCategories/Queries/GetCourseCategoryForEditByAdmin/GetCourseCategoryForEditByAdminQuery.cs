using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoryForEditByAdmin
{
    public record GetCourseCategoryForEditByAdminQuery(Guid CategoryId) : IRequest<ErrorOr<CourseCategoryDto>>;
}
