using Azure;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Forum;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Forum.Persistence
{
    public class QuestionService : IQuestionService
    {
        private readonly GtAcademyDbContext _context;

        public QuestionService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistById(Guid questionId)
        {
            return await _context.ForumQuestions.AnyAsync(q => q.QuestionId == questionId);
        }

        public async Task<bool> ExistByTitle(string title)
        {
            return await _context.ForumQuestions.AnyAsync(q => q.Title == title);
        }

        public async Task<int> GetForumQuestionsCount()
        {
            return await _context.ForumQuestions.Where(q => q.IsPublic).CountAsync();
        }

        public async Task<List<ForumQuestion>> GetForumQuestionsListWithRelations(Guid? courseId, string titleSearch, int pageId, int take)
        {
            IQueryable<ForumQuestion> questions = _context.ForumQuestions.Where(q => q.IsPublic);

            if (courseId != null)
                questions = questions.Where(q => q.CourseId == courseId);

            if (!string.IsNullOrEmpty(titleSearch))
                questions = questions.Where(q => q.Title.Contains(titleSearch));

            questions = questions
                .OrderByDescending(q => q.CreateDate)
                .Skip((pageId - 1) * take).Take(take);

            return await questions.Include(q => q.Course).Include(q => q.User).Include(q => q.ForumAnswers).ToListAsync();
        }

        public async Task<List<ForumQuestion>> GetTicketsListWithRelations(Guid? courseId, string titleSearch, int pageId, int take, bool? isClosed)
        {
            IQueryable<ForumQuestion> questions = _context.ForumQuestions.Where(q => !q.IsPublic);

            if (courseId != null)
                questions = questions.Where(q => q.CourseId == courseId);

            if (isClosed != null)
                questions = questions.Where(q => q.IsClosed == isClosed);

            if (!string.IsNullOrEmpty(titleSearch))
                questions = questions.Where(q => q.Title.Contains(titleSearch));

            questions = questions
                .OrderByDescending(q => q.CreateDate)
                .Skip((pageId - 1) * take).Take(take);

            return await questions.Include(q => q.Course).Include(q => q.User).Include(q => q.ForumAnswers).ToListAsync();
        }

        public async Task<ForumQuestion?> GetForumQuestionWithRelations(Guid questionId)
        {
            return await _context.ForumQuestions
            .Include(q => q.Course)
            .Include(q => q.User)
            .Include(q => q.ForumAnswers)
            .ThenInclude(a => a.User)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);
        }

        public async Task<List<ForumQuestion>> GetRecentForumQuestionsWithRelations(int count)
        {
            IQueryable<ForumQuestion> questions = _context.ForumQuestions.Where(q => q.IsPublic);

            questions = questions
                .OrderByDescending(q => q.CreateDate).Take(count);

            return await questions.Include(q => q.Course).Include(q => q.User).Include(q => q.ForumAnswers).ToListAsync();
        }

        public async Task<List<ForumQuestion>> GetUsersForumQuestionsList(Guid userId)
        {
            return await _context.ForumQuestions
            .Include(q => q.Course)
            .Include(q => q.User)
            .Include(q => q.ForumAnswers)
            .Where(q => q.UserId == userId && q.IsPublic)
            .OrderByDescending(q => q.CreateDate)
            .ToListAsync();
        }

        public async Task<List<ForumQuestion>> GetUsersTicketsList(Guid userId)
        {
            return await _context.ForumQuestions
            .Include(q => q.Course)
            .Include(q => q.User)
            .Include(q => q.ForumAnswers)
            .Where(q => q.UserId == userId && !q.IsPublic)
            .OrderByDescending(q => q.CreateDate)
            .ToListAsync();
        }

        public async Task<bool> IsQuestionClosed(Guid questionId)
        {
            var question = await _context.ForumQuestions.FindAsync(questionId);
            return question!.IsClosed;
        }

        public async Task<bool> IsQuestionPublic(Guid questionId)
        {
            var question = await _context.ForumQuestions.FindAsync(questionId);
            return question!.IsPublic;
        }

        public async Task<bool?> IsUserCreatorOfQuestion(Guid questionId, Guid userId)
        {
            var question = await _context.ForumQuestions.FindAsync(questionId);

            if (question == null) return null;

            return question.UserId == userId;
        }

        public async Task<int> GetTicketsCount()
        {
            return await _context.ForumQuestions.Where(q => !q.IsPublic).CountAsync();
        }
    }
}
