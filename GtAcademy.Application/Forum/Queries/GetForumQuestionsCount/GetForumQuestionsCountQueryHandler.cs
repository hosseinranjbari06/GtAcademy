using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.GetForumQuestionsCount
{
    public class GetForumQuestionsCountQueryHandler : IRequestHandler<GetForumQuestionsCountQuery, int>
    {
        private readonly IQuestionService _questionService;

        public GetForumQuestionsCountQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<int> Handle(GetForumQuestionsCountQuery request, CancellationToken cancellationToken)
        {
            return await _questionService.GetForumQuestionsCount();
        }
    }
}
