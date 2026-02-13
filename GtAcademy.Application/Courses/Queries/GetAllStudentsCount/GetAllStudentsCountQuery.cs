using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetAllStudentsCount
{
    public record GetAllStudentsCountQuery() : IRequest<int>;
}
