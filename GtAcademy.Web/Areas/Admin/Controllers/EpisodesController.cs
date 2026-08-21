using GtAcademy.Application.Admin.CourseCategories.Queries.GetCourseCategoriesListForAdmin;
using GtAcademy.Application.Admin.Episodes.Commands.CreateEpisode;
using GtAcademy.Application.Admin.Episodes.Commands.DeleteEpisode;
using GtAcademy.Application.Admin.Episodes.Commands.EditEpisode;
using GtAcademy.Application.Admin.Episodes.Queries.GetEpisodeForEdit;
using GtAcademy.Application.Admin.Episodes.Queries.GetTopicsEpisodes;
using GtAcademy.Application.Admin.Topics.Commands.CreateTopic;
using GtAcademy.Application.Admin.Topics.Commands.DeleteTopic;
using GtAcademy.Application.Admin.Topics.Commands.EditTopic;
using GtAcademy.Application.Admin.Topics.Queries.GetCoursesTopics;
using GtAcademy.Application.Admin.Topics.Queries.GetTopicForEdit;
using GtAcademy.Application.Admin.Users.Queries.GetTeachersListForAdmin;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Web.Security;
using GtAcademy.Web.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 2")]
    public class EpisodesController : Controller
    {
        private readonly IMediator _mediator;

        public EpisodesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Admin/Episodes/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            var result = await _mediator.Send(new GetTopicsEpisodesQuery(id));

            if (result.IsError) return NotFound();

            ViewBag.TopicId = id;
            return View(result.Value);
        }

        [Route("Admin/Episodes/Create/{id}")]
        public IActionResult Create(int id)
        {
            ViewBag.TopicId = id;
            return View();
        }

        [HttpPost("Admin/Episodes/Create")]
        public async Task<IActionResult> Create(CreateEpisodeDto episodeDto, IFormFile file)
        {
            if (file == null)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileName", "لطفا فایل اپیزود را انتخاب کنید");
                ViewBag.TopicId = episodeDto.TopicId;
                return View(episodeDto);
            }

            var fileValidation = FileManager.IsFileValid(file);

            if (fileValidation.IsError)
            {
                ModelState.Clear();
                ModelState.AddModelError("FileName", fileValidation.FirstError.Description);
                ViewBag.TopicId = episodeDto.TopicId;
                return View(episodeDto);
            }

            episodeDto.FileName = Path.GetFileName(file.FileName);

            var result = await _mediator.Send(new CreateEpisodeCommand(episodeDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    ModelState.Clear();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    ViewBag.TopicId = episodeDto.TopicId;
                    return View(episodeDto);
                }

                return NotFound();
            }

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"CourseFiles/{result.Value}/{episodeDto.TopicId}");
            await FileManager.SaveFile(file, path, episodeDto.FileName);

            return RedirectToAction(nameof(Index), new { id = episodeDto.TopicId });
        }

        [Route("Admin/Episodes/Edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _mediator.Send(new GetEpisodeForEditQuery(id));

            if (result.IsError) return NotFound();

            return View(result.Value);
        }

        [HttpPost("Admin/Episodes/Edit")]
        public async Task<IActionResult> Edit(EditEpisodeDto episodeDto, IFormFile file)
        {
            string oldFileName = "";

            if (file != null)
            {
                var fileValidation = FileManager.IsFileValid(file);

                if (fileValidation.IsError)
                {
                    ModelState.Clear();
                    ModelState.AddModelError("FileName", fileValidation.FirstError.Description);
                    ViewBag.TopicId = episodeDto.TopicId;
                    return View(episodeDto);
                }

                oldFileName = episodeDto.FileName;
                episodeDto.FileName = Path.GetFileName(file.FileName);
            }

            var result = await _mediator.Send(new EditEpisodeCommand(episodeDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    ModelState.Clear();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return View(episodeDto);
                }

                return NotFound();
            }

            if (file != null)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), $"CourseFiles/{result.Value}/{episodeDto.TopicId}");

                await FileManager.DeleteFile(path, oldFileName);
                await FileManager.SaveFile(file, path, episodeDto.FileName);
            }

            return RedirectToAction(nameof(Index), new { id = episodeDto.TopicId });
        }

        [HttpPost("Admin/Episodes/Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteEpisodeCommand(id));

            if (result.IsError) return NotFound();

            string path = Path.Combine(Directory.GetCurrentDirectory(), $"CourseFiles/{result.Value.Item2}/{result.Value.Item1}");
            await FileManager.DeleteFile(path, result.Value.Item3);

            return RedirectToAction(nameof(Index), new { id = result.Value.Item1 });
        }
    }
}
