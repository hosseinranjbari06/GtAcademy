using AutoMapper;
using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin
{
    public class EditCourseByAdminCommandHandler : IRequestHandler<EditCourseByAdminCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<Course> _genericCourseService;

        private readonly IGenericService<CourseCategory> _genericCategoryService;

        private readonly ICourseService _courseService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<EditCourseDto> _validator;

        public EditCourseByAdminCommandHandler(IGenericService<Course> genericCourseService, ICourseService courseService, IUnitOfWork unitOfWork, IValidator<EditCourseDto> validator, IGenericService<CourseCategory> genericCategoryService)
        {
            _genericCourseService = genericCourseService;
            _courseService = courseService;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _genericCategoryService = genericCategoryService;
        }

        public async Task<ErrorOr<Guid>> Handle(EditCourseByAdminCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CourseDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var course = await _courseService.GetCourseWithRelations(request.CourseDto.CourseId);

            if (course == null) return Error.NotFound();

            var category = await _genericCategoryService.GetByIdAsync(request.CourseDto.CategoryId);

            if (category == null)
            {
                return Error.Validation(code: "CategoryId", description: "دسته بندی انتخاب شده نامعتبر است");
            }

            course.CourseCategories.Clear();
            course.CourseCategories.Add(category);

            course.Title = request.CourseDto.Title;
            course.BannerName = request.CourseDto.BannerName;
            course.Description = request.CourseDto.Description;
            course.Tags = request.CourseDto.Tags;
            course.Price = request.CourseDto.Price;
            course.TeacherId = request.CourseDto.TeacherId;
            course.LastUpdateDate = DateTime.Now;

            _genericCourseService.Update(course);
            await _unitOfWork.CommitAsync();

            return course.CourseId;
        }
    }
}
