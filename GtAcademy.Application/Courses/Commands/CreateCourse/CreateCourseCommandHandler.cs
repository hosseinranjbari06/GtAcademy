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

namespace GtAcademy.Application.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<Course> _courseGenericService;

        private readonly IValidator<CreateCourseDto> _validator;

        private readonly IMapper _mapper;

        private readonly IUnitOfWork _unitOfWork;

        public CreateCourseCommandHandler(IValidator<CreateCourseDto> validator, IGenericService<Course> courseGenericService, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _courseGenericService = courseGenericService;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CourseDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            Course course = _mapper.Map<Course>(request.CourseDto);

            course.CourseId = Guid.NewGuid();
            course.CreateDate = DateTime.Now;
            course.LastUpdateDate = DateTime.Now;

            await _courseGenericService.AddAsync(course);
            await _unitOfWork.CommitAsync();

            return course.CourseId;
        }
    }
}
