using ErrorOr;
using FluentValidation;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Forum.Commands.AnswerQuestion;
using GtAcademy.Domain.Forum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Commands.AnswerTicket
{
    public class AnswerTicketCommandHandler : IRequestHandler<AnswerTicketCommand, ErrorOr<Guid>>
    {
        private readonly IValidator<CreateAnswerDto> _validator;

        private readonly IGenericService<ForumAnswer> _genericAnswerService;

        private readonly IQuestionService _questionService;

        private readonly IUnitOfWork _unitOfWork;

        public AnswerTicketCommandHandler(IValidator<CreateAnswerDto> validator, IGenericService<ForumAnswer> genericAnswerService, IQuestionService questionService, IUnitOfWork unitOfWork)
        {
            _validator = validator;
            _genericAnswerService = genericAnswerService;
            _questionService = questionService;
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorOr<Guid>> Handle(AnswerTicketCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request.AnswerDto);

            if (!validationResult.IsValid)
            {
                return validationResult.Errors
                    .Select(error => Error.Validation(code: error.PropertyName, description: error.ErrorMessage))
                    .ToList();
            }

            if (!await _questionService.ExistById(request.AnswerDto.QuestionId)) return Error.NotFound();

            if (await _questionService.IsQuestionClosed(request.AnswerDto.QuestionId)) return Error.Conflict();

            if (await _questionService.IsQuestionPublic(request.AnswerDto.QuestionId)) return Error.Conflict();

            var answer = new ForumAnswer()
            {
                AnswerId = Guid.NewGuid(),
                Content = request.AnswerDto.Content,
                CreateDate = DateTime.Now,
                IsAcceptedAnswer = false,
                QuestionId = request.AnswerDto.QuestionId,
                UserId = request.AnswerDto.UserId
            };

            await _genericAnswerService.AddAsync(answer);
            await _unitOfWork.CommitAsync();

            return answer.AnswerId;
        }
    }
}
