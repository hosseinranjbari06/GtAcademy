using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.CreateCourseCategoryByAdmin
{
    public record CreateCourseCategoryByAdminCommand(CourseCategoryDto CategoryDto) : IRequest<ErrorOr<Guid>>;
}
