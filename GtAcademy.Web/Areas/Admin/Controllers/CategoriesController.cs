using GtAcademy.Application.Admin.CourseCategories.Commands.CreateCourseCategoryByAdmin;
using GtAcademy.Application.Admin.CourseCategories.Commands.DeleteCourseCategoryByAdmin;
using GtAcademy.Application.Admin.CourseCategories.Commands.EditCourseCategoryByAdmin;
using GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoriesListForAdmin;
using GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoryForEditByAdmin;
using GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin;
using GtAcademy.Application.Admin.Users.Commands.DeleteUserByAdmin;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Infrastructure.Common.Persistence;
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
    public class CategoriesController : Controller
    {
        private readonly GtAcademyDbContext _context;

        private readonly IMediator _mediator;

        public CategoriesController(GtAcademyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _mediator.Send(new GetCourseCategoriesListForAdminQuery());
            return View(categories);
        }

        // GET: Admin/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCategoryDto categoryDto)
        {
            var result = await _mediator.Send(new CreateCourseCategoryByAdminCommand(categoryDto));

            if (result.IsError)
            {
                ModelState.Clear();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(categoryDto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetCourseCategoryForEditByAdminQuery((Guid)id));

            if (result.IsError) return NotFound();

            return View(result.Value);
        }

        // POST: Admin/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CourseCategoryDto categoryDto)
        {
            var result = await _mediator.Send(new EditCourseCategoryByAdminCommand(categoryDto));

            if (result.IsError)
            {
                ModelState.Clear();
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(categoryDto);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _mediator.Send(new DeleteCourseCategoryByAdminCommand(id));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}
