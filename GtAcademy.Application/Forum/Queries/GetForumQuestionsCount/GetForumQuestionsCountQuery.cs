using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsCount
{
    public record GetForumQuestionsCountQuery() : IRequest<int>;
}
