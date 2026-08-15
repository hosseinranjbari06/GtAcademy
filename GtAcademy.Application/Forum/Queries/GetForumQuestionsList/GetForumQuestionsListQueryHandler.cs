using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Forum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsList
{
    public class GetForumQuestionsListQueryHandler : IRequestHandler<GetForumQuestionsListQuery, List<ForumQuestionsListItemDto>>
    {
        private readonly IQuestionService _questionService;

        public GetForumQuestionsListQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<List<ForumQuestionsListItemDto>> Handle(GetForumQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionService.GetForumQuestionsListWithRelations(request.SearchDto.CourseId, request.SearchDto.TitleSearch, request.SearchDto.PageId, request.SearchDto.Take);

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
