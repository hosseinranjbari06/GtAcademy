using AutoMapper;
using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentDetailsForAdmin;
using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseCommentService : ICourseCommentService
    {
        private readonly GtAcademyDbContext _context;

        private readonly IMapper _mapper;

        public CourseCommentService(GtAcademyDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetCommentsCount()
        {
            return await _context.CourseComments.CountAsync();
        }

        public async Task<CourseComment?> GetCourseCommentById(Guid commentId)
        {
            return await _context.CourseComments
                .Include(comment => comment.Course)
                .FirstOrDefaultAsync(comment => comment.CommentId == commentId);
        }

        public async Task<SearchCourseCommentsListDto> GetCourseCommentsListForAdmin(SearchCourseCommentsListDto searchDto)
        {
            var comments = _context.CourseComments.Where(comment => comment.AdminSubmited == searchDto.AdminSubmited);

            if (searchDto.CourseId != null) comments = comments.Where(comment => comment.CourseId ==  searchDto.CourseId);

            searchDto.PagesCount = (int)Math.Ceiling((float)comments.Count() / (float)searchDto.Take);

            comments = comments.Skip((searchDto.PageId - 1) * searchDto.Take).Take(searchDto.Take);
            comments = comments
                .OrderByDescending(comment => comment.CreateDate)
                .Include(comment => comment.Course);

            var commentDtos = comments.Select(comment => new CourseCommentListItemDto()
            {
                CommentId = comment.CommentId,
                Content = (comment.Content.Length > 20) ? comment.Content.Substring(0, 20) + " ..." : comment.Content,
                CreateDate = comment.CreateDate,
                AdminSubmited = comment.AdminSubmited,
                CourseId = comment.CourseId,
                CourseTitle = comment.Course.Title,
                User = _mapper.Map<UserSummaryDto>(_context.Users.Find(comment.UserId))
            });

            searchDto.Comments = await commentDtos.ToListAsync();

            return searchDto;
        }
    }
}
