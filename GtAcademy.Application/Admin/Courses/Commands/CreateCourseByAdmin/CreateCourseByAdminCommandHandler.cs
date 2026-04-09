using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Roles;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin
{
    public class CreateCourseByAdminCommandHandler : IRequestHandler<CreateCourseByAdminCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<Course> _courseGenericService;

        private readonly IGenericService<CourseCategory> _categoryGenericService;

        private readonly IValidator<CreateCourseDto> _validator;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseByAdminCommandHandler(IValidator<CreateCourseDto> validator, IGenericService<Course> courseGenericService, IMapper mapper, IUnitOfWork unitOfWork, IGenericService<CourseCategory> categoryGenericService)
        {
            _validator = validator;
            _courseGenericService = courseGenericService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _categoryGenericService = categoryGenericService;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateCourseByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CourseDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            Course course = _mapper.Map<Course>(request.CourseDto);

            var category = await _categoryGenericService.GetByIdAsync(request.CourseDto.CategoryId);

            if (category == null)
            {
                return Error.Validation(code: "CategoryId", description: "دسته بندی انتخاب شده نامعتبر است");
            }

            course.CourseCategories = [category];
            course.CourseId = Guid.NewGuid();
            course.CreateDate = DateTime.Now;
            course.LastUpdateDate = DateTime.Now;

            await _courseGenericService.AddAsync(course);
            await _unitOfWork.CommitAsync();

            return course.CourseId;
        }
    }
}
