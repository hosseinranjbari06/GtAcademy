using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.DeleteCourseCategoryByAdmin
{
    public record DeleteCourseCategoryByAdminCommand(Guid CategoryId) : IRequest<ErrorOr<bool>>;
}
