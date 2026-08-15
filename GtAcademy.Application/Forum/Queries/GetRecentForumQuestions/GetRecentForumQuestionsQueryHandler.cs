using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetRecentForumQuestions
{
    public class GetRecentForumQuestionsQueryHandler : IRequestHandler<GetRecentForumQuestionsQuery, List<ForumQuestionsListItemDto>>
    {
        private readonly IQuestionService _questionService;

        public GetRecentForumQuestionsQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<List<ForumQuestionsListItemDto>> Handle(GetRecentForumQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionService.GetRecentForumQuestionsWithRelations(request.Count);

            return questions.Select(q => new ForumQuestionsListItemDto()
            {
                QuestionId = q.QuestionId,
                Title = q.Title,
                CourseId = q.CourseId,
                CreateDate = q.CreateDate,
                UserAvatarName = q.User.AvatarName,
                CourseTitle = q.Course.Title,
                AnswersCount = q.ForumAnswers.Count,
                IsClosed = q.IsClosed
            }).ToList();
        }
    }
}
