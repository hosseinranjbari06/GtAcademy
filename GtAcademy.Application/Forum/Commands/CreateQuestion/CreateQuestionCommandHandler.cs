using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Forum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, ErrorOr<Guid>>
    {
        private readonly IValidator<CreateQuestionDto> _validator;

        private readonly IQuestionService _questionService;

        private readonly ICourseService _courseService;

        private readonly IUserService _userService;

        private readonly IGenericService<ForumQuestion> _genericQuestionService;

        private readonly IUnitOfWork _unitOfWork;

        public CreateQuestionCommandHandler(IValidator<CreateQuestionDto> validator, IQuestionService questionService, ICourseService courseService, IUserService userService, IGenericService<ForumQuestion> genericQuestionService, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _questionService = questionService;
            _courseService = courseService;
            _userService = userService;
            _genericQuestionService = genericQuestionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.QuestionDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (! await _courseService.ExistByCourseId(request.QuestionDto.CourseId)) return Error.NotFound();

            if (! await _userService.ExistById(request.QuestionDto.UserId)) return Error.NotFound();

            if (await _questionService.ExistByTitle(request.QuestionDto.Title)) return Error.Validation(code: "Title", description: "عنوان وارد شده تکراری میباشد");

            var question = new ForumQuestion()
            {
                QuestionId = Guid.NewGuid(),
                Title = request.QuestionDto.Title,
                Content = request.QuestionDto.Content,
                CourseId = request.QuestionDto.CourseId,
                UserId = request.QuestionDto.UserId,
                CreateDate = DateTime.Now,
                IsPublic = !request.QuestionDto.IsTicket,
                IsClosed = false
            };

            await _genericQuestionService.AddAsync(question);
            await _unitOfWork.CommitAsync();

            return question.QuestionId;
        }
    }
}
