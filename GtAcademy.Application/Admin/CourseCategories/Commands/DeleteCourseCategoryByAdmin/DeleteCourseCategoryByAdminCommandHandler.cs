using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.DeleteCourseCategoryByAdmin
{
    public class DeleteCourseCategoryByAdminCommandHandler : IRequestHandler<DeleteCourseCategoryByAdminCommand, ErrorOr<bool>>
    {
        private readonly ICourseCategoryService _courseCategoryService;

        private readonly IGenericService<CourseCategory> _genericCourseCategoryService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteCourseCategoryByAdminCommandHandler(IUnitOfWork unitOfWork, ICourseCategoryService courseCategoryService, IGenericService<CourseCategory> genericCourseCategoryService)
        {
            _unitOfWork = unitOfWork;
            _courseCategoryService = courseCategoryService;
            _genericCourseCategoryService = genericCourseCategoryService;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteCourseCategoryByAdminCommand request, CancellationToken cancellationToken)
        {
            var category = await _courseCategoryService.GetCourseCategoryForEditById(request.CategoryId);

            if (category == null) return Error.NotFound();

            category.Courses.Clear();
            _genericCourseCategoryService.Delete(category);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
