using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CreateQuestion
{
    public record CreateQuestionCommand(CreateQuestionDto QuestionDto) : IRequest<ErrorOr<Guid>>;
}
