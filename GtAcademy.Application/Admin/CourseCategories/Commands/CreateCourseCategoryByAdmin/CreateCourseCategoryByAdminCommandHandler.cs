using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Admin.Users.Commands.CreateUserByAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GtAcademy.Application.Admin.CourseCategories.Commands.CreateCourseCategoryByAdmin
{
    public class CreateCourseCategoryByAdminCommandHandler : IRequestHandler<CreateCourseCategoryByAdminCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<CourseCategory> _genericCategoryService;

        private readonly ICourseCategoryService _courseCategoryService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CourseCategoryDto> _validator;

        private readonly IMapper _mapper;

        public CreateCourseCategoryByAdminCommandHandler(IGenericService<CourseCategory> genericCategoryService, IMapper mapper, IValidator<CourseCategoryDto> validator, ICourseCategoryService courseCategoryService, IUnitOfWork unitOfWork)
        {
            _genericCategoryService = genericCategoryService;
            _mapper = mapper;
            _validator = validator;
            _courseCategoryService = courseCategoryService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateCourseCategoryByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CategoryDto);

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

            request.CategoryDto.CategoryId = Guid.NewGuid();
            CourseCategory category = _mapper.Map<CourseCategory>(request.CategoryDto);

            await _genericCategoryService.AddAsync(category);
            await _unitOfWork.CommitAsync();

            return category.CategoryId;
        }
    }
}
