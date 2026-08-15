using AutoMapper;
using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Forum.Queries.GetForumQuestionDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Tickets.Queries.GetTicketDetails
{
    public class GetTicketDetailsQueryHandler : IRequestHandler<GetTicketDetailsQuery, ErrorOr<QuestionDetailsDto>>
    {
        private readonly IQuestionService _questionService;

        private readonly IMapper _mapper;

        public GetTicketDetailsQueryHandler(IQuestionService questionService, IMapper mapper)
        {
            _questionService = questionService;
            _mapper = mapper;
        }

        public async Task<ErrorOr<QuestionDetailsDto>> Handle(GetTicketDetailsQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _questionService.GetForumQuestionWithRelations(request.TicketId);

            if (ticket == null || ticket.IsPublic) return Error.NotFound();

            var ticketDto = _mapper.Map<QuestionDetailsDto>(ticket);

            ticketDto.CourseTitle = ticket.Course.Title;
            ticketDto.UserAvatarName = ticket.User.AvatarName;
            ticketDto.UserName = ticket.User.UserName;

            ticketDto.ForumAnswerDtos = ticket.ForumAnswers.Select(answer => new AnswerDetailsDto()
            {
                AnswerId = answer.AnswerId,
                Content = answer.Content,
                CreateDate = answer.CreateDate,
                IsAcceptedAnswer = answer.IsAcceptedAnswer,
                UserName = answer.User.UserName,
                UserAvatarName = answer.User.AvatarName
            }).OrderBy(answer => answer.CreateDate).ToList();

            return ticketDto;
        }
    }
}
