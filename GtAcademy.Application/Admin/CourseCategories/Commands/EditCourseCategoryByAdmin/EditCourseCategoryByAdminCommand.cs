using ErrorOr;
using GtAcademy.Application.Courses.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.EditCourseCategoryByAdmin
{
    public record EditCourseCategoryByAdminCommand(CourseCategoryDto CategoryDto) : IRequest<ErrorOr<bool>>;
}
