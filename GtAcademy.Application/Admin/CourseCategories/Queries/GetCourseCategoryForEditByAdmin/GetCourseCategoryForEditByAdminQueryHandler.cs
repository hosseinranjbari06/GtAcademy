using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoryForEditByAdmin
{
    public class GetCourseCategoryForEditByAdminQueryHandler : IRequestHandler<GetCourseCategoryForEditByAdminQuery, ErrorOr<CourseCategoryDto>>
    {
        private readonly IGenericService<CourseCategory> _genericCategoryService;

        private readonly IMapper _mapper;

        public GetCourseCategoryForEditByAdminQueryHandler(IGenericService<CourseCategory> genericCategoryService, IMapper mapper)
        {
            _genericCategoryService = genericCategoryService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<CourseCategoryDto>> Handle(GetCourseCategoryForEditByAdminQuery request, CancellationToken cancellationToken)
        {
            var category = await _genericCategoryService.GetByIdAsync(request.CategoryId);

            if (category == null) return Error.NotFound();

            return _mapper.Map<CourseCategoryDto>(category);
        }
    }
}
