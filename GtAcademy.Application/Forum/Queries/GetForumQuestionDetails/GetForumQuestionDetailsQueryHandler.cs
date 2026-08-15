using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionDetails
{
    public class GetForumQuestionDetailsQueryHandler : IRequestHandler<GetForumQuestionDetailsQuery, ErrorOr<QuestionDetailsDto>>
    {
        private readonly IQuestionService _questionService;

        private readonly IMapper _mapper;

        public GetForumQuestionDetailsQueryHandler(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<QuestionDetailsDto>> Handle(GetForumQuestionDetailsQuery request, CancellationToken cancellationToken)
        {
            var question = await _questionService.GetForumQuestionWithRelations(request.QuestionId);

            if (question == null || !question.IsPublic) return Error.NotFound();

            var questionDto = _mapper.Map<QuestionDetailsDto>(question);

            questionDto.CourseTitle = question.Course.Title;
            questionDto.UserAvatarName = question.User.AvatarName;
            questionDto.UserName = question.User.UserName;

            questionDto.ForumAnswerDtos = question.ForumAnswers.Select(answer => new AnswerDetailsDto()
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                CreateDate = answer.CreateDate,
                IsAcceptedAnswer = answer.IsAcceptedAnswer,
                UserName = answer.User.UserName,
                UserAvatarName = answer.User.AvatarName
            }).OrderBy(answer => answer.CreateDate).ToList();

            return questionDto;
        }
    }
}
