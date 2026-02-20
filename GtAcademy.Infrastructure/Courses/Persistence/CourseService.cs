using AutoMapper;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseService : ICourseService
    {
        private readonly GtAcademyDbContext _context;

        private readonly IMapper _mapper;

        private readonly IUserService _userService;

        public CourseService(GtAcademyDbContext context, IUserService userService, IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<int> GetAllStudentsCount()
        {
            int studentsCount = 0;

            var courseIds = await _context.Courses.Select(course => course.CourseId).ToListAsync();

            foreach (var courseId in courseIds)
            {
                studentsCount += await GetCourseStudentsCount(courseId);
            }

            return studentsCount;
        }

        public async Task<List<CourseCategoryDto>> GetCourseCategories()
        {
            return await _context.CourseCategories
                .Select(category => _mapper.Map<CourseCategoryDto>(category))
                .ToListAsync();
        }

        public async Task<List<CourseCommentDto>> GetCourseCommentDtos(Guid courseId)
        {
            return await _context.CourseComments
                .Where(cc => cc.CourseId == courseId)
                .Include(cc => cc.User)
                .Select(cc => _mapper.Map<CourseCommentDto>(cc))
                .ToListAsync();
        }

        public async Task<List<CourseSummaryDto>> GetCoursesList(string search = "", string categoryTitle = "", int seperate = 6, int pageId = 1)
        {
            IQueryable<Course> courses = _context.Courses.Include(course => course.CourseCategories);

            if (!string.IsNullOrEmpty(search))
                courses = courses
                    .Where(course => course.Title.Contains(search) || course.Tags.Contains(search));

            if (!string.IsNullOrEmpty(categoryTitle))
                courses = courses
                    .Where(course => course.CourseCategories.Any(category => category.Title == categoryTitle));

            if (seperate > 0 && pageId > 0)
                courses = courses
                    .Skip(seperate * (pageId - 1))
                    .Take(seperate);

            courses = courses.OrderByDescending(course => course.LastUpdateDate);

            var courseDtos = await courses
                .Select(course => _mapper.Map<CourseSummaryDto>(course))
                .ToListAsync();

            courseDtos.ForEach(dto =>
                {
                    dto.TeacherSummary = _userService.GetUserSummary(dto.TeacherId).Result;
                });

            return courseDtos;
        }

        public async Task<int> GetCourseStudentsCount(Guid courseId)
        {
            var course = await _context.Courses
                .Where(course => course.CourseId == courseId)
                .Include(course => course.Orders)
                .FirstAsync();

            return course.Orders.Where(order => order.IsPaid).Count();
        }

        public async Task<Course?> GetCourseWithEpisodes(Guid courseId)
        {
            return await _context.Courses
                .Where(course => course.CourseId == courseId)
                .Include(course => course.CourseCategories)
                .Include(course => course.Topics)
                .ThenInclude(topic => topic.Episodes)
                .FirstOrDefaultAsync();
        }

        public async Task<List<CourseSummaryDto>> GetPopularCoursesList(int take)
        {
            var courseDtos = await _context.Courses
                .Include(course => course.Orders)
                .Include(course => course.CourseCategories)
                .OrderByDescending(course => course.Orders.Where(order => order.IsPaid).Count())
                .Take(take)
                .Select(course => _mapper.Map<CourseSummaryDto>(course))
                .ToListAsync();

            courseDtos.ForEach(dto =>
            {
                dto.TeacherSummary = _userService.GetUserSummary(dto.TeacherId).Result;
            });

            return courseDtos;
        }

        public async Task<bool> IsCourseExist(Guid courseId)
        {
            return await _context.Courses.AnyAsync(course => course.CourseId == courseId);
        }
    }
}
