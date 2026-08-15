using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsList
{
    public record GetForumQuestionsListQuery(ForumQuestionsSearchDto SearchDto) : IRequest<List<ForumQuestionsListItemDto>>;
}
