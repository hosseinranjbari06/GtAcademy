using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetRecentForumQuestions
{
    public record GetRecentForumQuestionsQuery(int Count = 5) : IRequest<List<ForumQuestionsListItemDto>>;
}
