using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Commands.CreateCourseComment;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Courses.Commands.CreateComment
{
    public class CreateCourseCommentCommandHandler : IRequestHandler<CreateCourseCommentCommand, ErrorOr<Guid>>
    {
        private readonly IGenericService<CourseComment> _genericCommentService;

        private readonly IGenericService<Course> _genericCourseService;

        private readonly IGenericService<User> _genericUserService;

        private readonly ICourseService _courseService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IValidator<CreateCourseCommentDto> _validator;

        public CreateCourseCommentCommandHandler(IGenericService<CourseComment> genericCommentService, ICourseService courseService, IUnitOfWork unitOfWork, IValidator<CreateCourseCommentDto> validator, IGenericService<Course> genericCourseService, IGenericService<User> genericUserService)
        {
            _genericCommentService = genericCommentService;
            _courseService = courseService;
            _unitOfWork = unitOfWork;
            _validator = validator;
            _genericCourseService = genericCourseService;
            _genericUserService = genericUserService;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateCourseCommentCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.CommentDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            var course = await _genericCourseService.GetByIdAsync(request.CommentDto.CourseId);
            if (course == null) return Error.NotFound();

            var user = await _genericUserService.GetByIdAsync(request.CommentDto.UserId);
            if (user == null) return Error.NotFound();

            var comment = new CourseComment()
            {
                CommentId = Guid.NewGuid(),
                Content = request.CommentDto.Content,
                CourseId = request.CommentDto.CourseId,
                UserId = request.CommentDto.UserId,
                CreateDate = DateTime.Now,
                AdminSubmited = false,
                User = user,
                Course = course
            };

            await _genericCommentService.AddAsync(comment);
            await _unitOfWork.CommitAsync();

            return comment.CourseId;
        }
    }
}
