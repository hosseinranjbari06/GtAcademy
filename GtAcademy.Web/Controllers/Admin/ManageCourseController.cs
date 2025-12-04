using ErrorOr;
using GtAcademy.Application.Courses.Commands.CreateCourse;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Web.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Controllers.Admin
{
    [Authorize]
    public class ManageCourseController : Controller
    {
        private readonly IMediator _mediator;

        public ManageCourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult CreateCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto courseDto, IFormFile file)
        {
            var fileValidation = FileManager.IsFileValid(file);

            if (fileValidation.IsError)
            {
                ModelState.AddModelError(fileValidation.FirstError.Code, fileValidation.FirstError.Description);
                return View(courseDto);
            }

            courseDto.BannerName = FileManager.GenerateRandomFileName(file.FileName);

            var result = await _mediator.Send(new CreateCourseCommand(courseDto));

            if (result.IsError)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return View(courseDto);
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            await FileManager.SaveFile(file, path, courseDto.BannerName);

            return RedirectToAction("Index", "ManageCourse");
        }
    }
}
