using AutoMapper;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoriesListForAdmin
{
    public class GetCourseCategoriesListForAdminQueryHandler : IRequestHandler<GetCourseCategoriesListForAdminQuery, List<CourseCategoryDto>>
    {
        private readonly IGenericService<CourseCategory> _genericCategoryService;

        private readonly IMapper _mapper;

        public GetCourseCategoriesListForAdminQueryHandler(IGenericService<CourseCategory> genericCategoryService, IMapper mapper)
        {
            _genericCategoryService = genericCategoryService;
            _mapper = mapper;
        }

        public async Task<List<CourseCategoryDto>> Handle(GetCourseCategoriesListForAdminQuery request, CancellationToken cancellationToken)
        {
            var categories = await _genericCategoryService.GetAllAsync();
            return [.. categories.Select(category => _mapper.Map<CourseCategoryDto>(category))];
        }
    }
}
