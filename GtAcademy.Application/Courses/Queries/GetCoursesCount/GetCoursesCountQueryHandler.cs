using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetCoursesCount
{
    public class GetCoursesCountQueryHandler : IRequestHandler<GetCoursesCountQuery, int>
    {
        private readonly IGenericService<Course> _courseGenericService;

        public GetCoursesCountQueryHandler(IGenericService<Course> courseGenericService)
        {
            _courseGenericService = courseGenericService;
        }

        public async Task<int> Handle(GetCoursesCountQuery request, CancellationToken cancellationToken)
        {
            return await _courseGenericService.GetCountAsync();
        }
    }
}
