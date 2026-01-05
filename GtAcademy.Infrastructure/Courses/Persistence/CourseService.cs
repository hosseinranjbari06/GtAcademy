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

        public async Task<List<CourseSummaryDto>> GetCoursesList(string search = "", int seperate = 6, int pageId = 1)
        {
            IQueryable<Course> courses = _context.Courses;

            if (!string.IsNullOrEmpty(search))
                courses = courses
                    .Where(course => course.Title.Contains(search) || course.Tags.Contains(search));

            if (seperate > 0 && pageId > 0)
                courses = courses
                    .Skip(seperate * (pageId - 1))
                    .Take(seperate);

            courses = courses.OrderByDescending(course => course.LastUpdateDate);

            var courseDtos = await courses
                .Select(course => _mapper.Map<CourseSummaryDto>(course))
                .ToListAsync();

            courseDtos.ForEach( dto =>
                {
                    dto.TeacherSummary =  _userService.GetUserSummary(dto.TeacherId).Result;
                });

            return courseDtos;
        }
    }
}
