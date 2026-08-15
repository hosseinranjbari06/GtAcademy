using ErrorOr;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Forum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Forum.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, ErrorOr<bool>>
    {
        private readonly IQuestionService _questionService;

        private readonly IPermissionService _permissionService;

        private readonly IGenericService<ForumAnswer> _genericAnswerService;

        private readonly IGenericService<ForumQuestion> _genericQuestionService;

        private readonly IUnitOfWork _unitOfWork;

        public DeleteQuestionCommandHandler(IQuestionService questionService, IGenericService<ForumAnswer> genericAnswerService, IGenericService<ForumQuestion> genericQuestionService, IUnitOfWork unitOfWork, IPermissionService permissionService)
        {
            _questionService = questionService;
            _genericAnswerService = genericAnswerService;
            _genericQuestionService = genericQuestionService;
            _unitOfWork = unitOfWork;
            _permissionService = permissionService;
        }

        public async Task<ErrorOr<bool>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionService.GetForumQuestionWithRelations(request.QuestionId);

            if (question == null) return Error.NotFound();

            var isUserCreatorOfQuestion = (question.UserId == request.UserId);
            var isUserAdmin = await _permissionService.UserHasAnyRole(request.UserId);

            if (isUserCreatorOfQuestion || isUserAdmin)
            {
                foreach (var answer in question.ForumAnswers)
                {
                    _genericAnswerService.Delete(answer);
                }

                _genericQuestionService.Delete(question);
                await _unitOfWork.CommitAsync();

                return true;
            }

            return Error.Unauthorized();
        }
    }
}
