using GtAcademy.Domain.Forum;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Common.Interfaces
{
    public interface IQuestionService
    {
        Task<List<ForumQuestion>> GetForumQuestionsListWithRelations(Guid? courseId, string titleSearch, int pageId, int take);

        Task<List<ForumQuestion>> GetTicketsListWithRelations(Guid? courseId, string titleSearch, int pageId, int take, bool? isClosed);

        Task<List<ForumQuestion>> GetRecentForumQuestionsWithRelations(int count);

        Task<List<ForumQuestion>> GetUsersForumQuestionsList(Guid userId);

        Task<List<ForumQuestion>> GetUsersTicketsList(Guid userId);

        Task<ForumQuestion?> GetForumQuestionWithRelations(Guid questionId);

        Task<int> GetForumQuestionsCount();

        Task<int> GetTicketsCount();

        Task<bool> ExistByTitle(string title);

        Task<bool> ExistById(Guid questionId);

        Task<bool> IsQuestionClosed(Guid questionId);

        Task<bool> IsQuestionPublic(Guid questionId);

        Task<bool?> IsUserCreatorOfQuestion(Guid questionId, Guid userId);
    }
}
