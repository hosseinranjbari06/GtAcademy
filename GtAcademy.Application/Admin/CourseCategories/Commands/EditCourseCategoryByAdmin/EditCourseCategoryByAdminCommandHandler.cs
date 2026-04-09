using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.EditCourseCategoryByAdmin
{
    public class EditCourseCategoryByAdminCommandHandler : IRequestHandler<EditCourseCategoryByAdminCommand, ErrorOr<bool>>
    {
        private readonly IGenericService<CourseCategory> _genericCategoryService;

        private readonly ICourseCategoryService _courseCategoryService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<EditCourseCategoryByAdminCommand> _validator;

        public EditCourseCategoryByAdminCommandHandler(IGenericService<CourseCategory> genericCategoryService, ICourseCategoryService courseCategoryService, IUnitOfWork unitOfWork, IValidator<EditCourseCategoryByAdminCommand> validator)
        {
            _genericCategoryService = genericCategoryService;
            _courseCategoryService = courseCategoryService;
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<ErrorOr<bool>> Handle(EditCourseCategoryByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => ErrorOr.Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (await _courseCategoryService.IsCategoryTitleExist(request.CategoryDto.Title))
            {
                return ErrorOr.Error.Validation(code: "Title", description: "عنوان وارد شده تکراری میباشد");
            }

            var category = await _courseCategoryService.GetCourseCategoryForEditById(request.CategoryDto.CategoryId);

            if (category == null) return Error.NotFound();

            category.Title = request.CategoryDto.Title;

            _genericCategoryService.Update(category);
            await _unitOfWork.CommitAsync();

            return true;
        }
    }
}
