using GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin;
using GtAcademy.Application.Admin.Users.Commands.CreateUserByAdmin;
using GtAcademy.Application.Admin.Users.Commands.DeleteUserByAdmin;
using GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetUserDetailsForAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetUserForEditByAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin;
using GtAcademy.Application.Users.Common;
using GtAcademy.Application.Users.Queries.GetUserAvatarNameById;
using GtAcademy.Application.Users.Queries.GetUsersCount;
using GtAcademy.Domain.Users;
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
    [CheckPermission("1 4")]
    public class UsersController : Controller
    {
        private readonly GtAcademyDbContext _context;

        private readonly IMediator _mediator;

        public UsersController(GtAcademyDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        // GET: Admin/Users
        public async Task<IActionResult> Index(SearchUsersListDto search)
        {
            var result = await _mediator.Send(new GetUsersListForAdminQuery(search));
            int usersCount = await _mediator.Send(new GetUsersCountQuery());
            ViewBag.Search = search;
            ViewBag.PagesCount = Math.Ceiling((float)usersCount / (float)search.Take);
            ViewBag.CurrentPage = search.PageId;

            return View(result);
        }

        // GET: Admin/Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDto = await _mediator.Send(new GetUserDetailsForAdminQuery((Guid)id));

            if (userDto.IsError) return NotFound();

            return View(userDto.Value);
        }

        // GET: Admin/Users/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _mediator.Send(new GetRolesForAdminQuery());

            return View();
        }

        // POST: Admin/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserDto userDto)
        {
            var result = await _mediator.Send(new CreateUserByAdminCommand(userDto));

            if (result.IsError)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                ViewBag.Roles = await _mediator.Send(new GetRolesForAdminQuery());

                return View(userDto);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _mediator.Send(new GetUserForEditByAdminQuery((Guid)id));

            if (result.IsError) return NotFound();

            ViewBag.Roles = await _mediator.Send(new GetRolesForAdminQuery());

            return View(result.Value);
        }

        // POST: Admin/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserDto userDto)
        {
            var avatarResult = await _mediator.Send(new GetUserAvatarNameByIdQuery(userDto.UserId));
            var result = await _mediator.Send(new EditUserByAdminCommand(userDto));

            if (result.IsError)
            {
                if (result.FirstError.Type != ErrorOr.ErrorType.Validation)
                {
                    return NotFound();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    ViewBag.Roles = await _mediator.Send(new GetRolesForAdminQuery());

                    return View(userDto);
                }
            }

            if (userDto.DeleteAvatar && !avatarResult.IsError)
            {
                if (avatarResult.Value != "default.jpg")
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\img\\users");
                    await FileManager.DeleteFile(path, avatarResult.Value);
                }
            }

            return RedirectToAction(nameof(Details), new { id = result.Value });
        }

        // GET: Admin/Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userDto = await _mediator.Send(new GetUserDetailsForAdminQuery((Guid)id));

            if (userDto.IsError) return NotFound();

            return View(userDto.Value);
        }

        // POST: Admin/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var result = await _mediator.Send(new DeleteUserByAdminCommand(id));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(Index));
        }
    }
}
