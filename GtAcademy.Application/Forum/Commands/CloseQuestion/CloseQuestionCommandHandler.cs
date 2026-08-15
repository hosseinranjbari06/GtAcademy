using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Forum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.CloseQuestion
{
    public class CloseQuestionCommandHandler : IRequestHandler<CloseQuestionCommand, ErrorOr<Guid>>
    {
        private readonly IQuestionService _questionService;

        private readonly IPermissionService _permissionService;

        private readonly IGenericService<ForumQuestion> _genericQuestionService;

        private readonly IUnitOfWork _unitOfWork;

        public CloseQuestionCommandHandler(IQuestionService questionService, IUnitOfWork unitOfWork, IGenericService<ForumQuestion> genericQuestionService, IPermissionService permissionService)
        {
            _questionService = questionService;
            _unitOfWork = unitOfWork;
            _genericQuestionService = genericQuestionService;
            _permissionService = permissionService;
        }

        public async Task<ErrorOr<Guid>> Handle(CloseQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionService.GetForumQuestionWithRelations(request.QuestionId);

            if (question == null) return Error.NotFound();

            var isUserCreatorOfQuestion = (question.UserId == request.UserId);
            var isUserAdmin = await _permissionService.UserHasAnyRole(request.UserId);

            if (isUserCreatorOfQuestion || isUserAdmin)
            {
                question.IsClosed = true;

                _genericQuestionService.Update(question);
                await _unitOfWork.CommitAsync();

                return question.QuestionId;
            }

            return Error.Unauthorized();
        }
    }
}
