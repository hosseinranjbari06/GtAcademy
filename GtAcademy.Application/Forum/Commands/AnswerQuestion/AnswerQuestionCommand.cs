using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.AnswerQuestion
{
    public record AnswerQuestionCommand(CreateAnswerDto AnswerDto) : IRequest<ErrorOr<Guid>>;
}
