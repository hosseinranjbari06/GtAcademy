using ErrorOr;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Admin.Topics.Commands.DeleteTopic
{
    public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, ErrorOr<Guid>>
    {
        public Task<ErrorOr<Guid>> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
