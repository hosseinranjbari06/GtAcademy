using GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoriesListForAdmin;
using GtAcademy.Application.Admin.Courses.Commands.CreateCourseByAdmin;
using GtAcademy.Application.Admin.Courses.Commands.DeleteCourseByAdmin;
using GtAcademy.Application.Admin.Courses.Commands.EditCourseByAdmin;
using GtAcademy.Application.Admin.Courses.Queries.GetCourseForEditByAdmin;
using GtAcademy.Application.Admin.Courses.Queries.GetCoursesListForAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetTeachersListForAdmin;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Courses.Queries.GetCoursesCount;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
using GtAcademy.Web.Security;
using GtAcademy.Web.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 2")]
    public class CoursesController : Controller
    {
        private readonly GtAcademyDbContext _context;

        private readonly IMediator _mediator;

        public CoursesController(GtAcademyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: Admin/Courses
        public async Task<IActionResult> Index(SearchCourseListDto search)
        {
            var courses = await _mediator.Send(new GetCoursesListForAdminQuery(search));
            var coursesCount = await _mediator.Send(new GetCoursesCountQuery());

            ViewBag.Search = search;
            ViewBag.PagesCount = Math.Ceiling((float)coursesCount / (float)search.Take);
            ViewBag.CurrentPage = search.PageId;

            return View(courses);
        }

        // GET: Admin/Courses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Admin/Courses/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
            ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

            return View();
        }

        // POST: Admin/Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDto courseDto, IFormFile bannerFile)
        {
            var fileValidation = FileManager.IsFileValid(bannerFile);

            if (fileValidation.IsError)
            {
                ModelState.Clear();
                ModelState.AddModelError("BannerName", fileValidation.FirstError.Description);
                ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
                ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

                return View(courseDto);
            }

            courseDto.BannerName = FileManager.GenerateRandomFileName(bannerFile.FileName);

            var result = await _mediator.Send(new CreateCourseByAdminCommand(courseDto));

            if (result.IsError)
            {
                ModelState.Clear();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
                ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

                return View(courseDto);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/assets/img/courses");
            await FileManager.SaveFile(bannerFile, path, courseDto.BannerName);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Courses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetCourseForEditByAdminQuery((Guid)id));

            if (result.IsError) return NotFound();

            ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
            ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

            return View(result.Value);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCourseDto courseDto, IFormFile? bannerFile)
        {
            string oldBannerName = "";

            if (bannerFile != null)
            {
                var fileValidation = FileManager.IsFileValid(bannerFile);

                if (fileValidation.IsError)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("BannerName", fileValidation.FirstError.Description);
                    ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
                    ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

                    return View(courseDto);
                }
                oldBannerName = courseDto.BannerName;
                courseDto.BannerName = FileManager.GenerateRandomFileName(bannerFile.FileName);
            }

            var result = await _mediator.Send(new EditCourseByAdminCommand(courseDto));

            if (result.IsError)
            {
                ModelState.Clear();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                ViewBag.Teachers = await _mediator.Send(new GetTeachersListForAdminQuery());
                ViewBag.Categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());

                return View(courseDto);
            }

            if (!string.IsNullOrEmpty(oldBannerName))
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\img\\courses");

                await FileManager.DeleteFile(path, oldBannerName);
                await FileManager.SaveFile(bannerFile!, path, courseDto.BannerName);
            }

            return RedirectToAction(nameof(Details), new { id = result.Value });
        }

        // GET: Admin/Courses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Admin/Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _mediator.Send(new DeleteCourseByAdminCommand(id));

            if (result.IsError) return NotFound();

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"CourseFiles/{id}");

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}
