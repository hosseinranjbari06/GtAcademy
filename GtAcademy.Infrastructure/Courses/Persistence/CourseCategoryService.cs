using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Infrastructure.Courses.Persistence
{
    public class CourseCategoryService : ICourseCategoryService
    {
        private readonly GtAcademyDbContext _context;

        public CourseCategoryService(GtAcademyDbContext context)
        {
            _context = context;
        }

        public async Task<CourseCategory?> GetCourseCategoryForEditById(Guid categoryId)
        {
            return await _context.CourseCategories
                .Include(category => category.Courses)
                .FirstOrDefaultAsync(category => category.CategoryId == categoryId);
        }

        public async Task<bool> IsCategoryTitleExist(string title)
        {
            return await _context.CourseCategories.AnyAsync(category => category.Title == title);
        }
    }
}
