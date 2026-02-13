using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Queries.GetAllStudentsCount
{
    public class GetAllStudentsCountQueryHandler : IRequestHandler<GetAllStudentsCountQuery, int>
    {
        private readonly ICourseService _courseService;

        public GetAllStudentsCountQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<int> Handle(GetAllStudentsCountQuery request, CancellationToken cancellationToken)
        {
            return await _courseService.GetAllStudentsCount();
        }
    }
}
