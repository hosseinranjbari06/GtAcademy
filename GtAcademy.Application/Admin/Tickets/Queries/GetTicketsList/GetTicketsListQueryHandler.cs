using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Tickets.Queries.GetTicketsList
{
    public class GetTicketsListQueryHandler : IRequestHandler<GetTicketsListQuery, List<ForumQuestionsListItemDto>>
    {
        private readonly IQuestionService _questionService;

        public GetTicketsListQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<List<ForumQuestionsListItemDto>> Handle(GetTicketsListQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionService.GetTicketsListWithRelations(request.SearchDto.CourseId, request.SearchDto.TitleSearch, request.SearchDto.PageId, request.SearchDto.Take, request.SearchDto.IsClosed);

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
