using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Queries.CanUserChangeQuestionStatus
{
    public class CanUserChangeQuestionStatusQueryHandler : IRequestHandler<CanUserChangeQuestionStatusQuery, ErrorOr<bool>>
    {
        private readonly IQuestionService _questionService;

        private readonly IPermissionService _permissionService;

        public CanUserChangeQuestionStatusQueryHandler(IQuestionService questionService, IPermissionService permissionService)
        {
            _questionService = questionService;
            _permissionService = permissionService;
        }

        public async Task<ErrorOr<bool>> Handle(CanUserChangeQuestionStatusQuery request, CancellationToken cancellationToken)
        {
            var isUserCreatorOfQuestion = await _questionService.IsUserCreatorOfQuestion(request.QuestionId, request.UserId);

            if (isUserCreatorOfQuestion == null) return Error.NotFound();

            var isUserAdmin = await _permissionService.UserHasAnyRole(request.UserId);

            return (bool)isUserCreatorOfQuestion || isUserAdmin;
        }
    }
}
