using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Tickets.Queries.GetTicketsCount
{
    public class GetTicketsCountQueryHandler : IRequestHandler<GetTicketsCountQuery, int>
    {
        private readonly IQuestionService _questionService;

        public GetTicketsCountQueryHandler(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        public async Task<int> Handle(GetTicketsCountQuery request, CancellationToken cancellationToken)
        {
            return await _questionService.GetTicketsCount();
        }
    }
}
