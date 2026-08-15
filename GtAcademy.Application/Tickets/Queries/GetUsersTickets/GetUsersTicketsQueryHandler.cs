using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Queries.GetUsersTickets
{
    public class GetUsersTicketsQueryHandler : IRequestHandler<GetUsersTicketsQuery, ErrorOr<List<ForumQuestionsListItemDto>>>
    {
        private readonly IQuestionService _questionService;

        public GetUsersTicketsQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<ErrorOr<List<ForumQuestionsListItemDto>>> Handle(GetUsersTicketsQuery request, CancellationToken cancellationToken)
        {
            var questions = await _questionService.GetUsersTicketsList(request.UserId);

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
