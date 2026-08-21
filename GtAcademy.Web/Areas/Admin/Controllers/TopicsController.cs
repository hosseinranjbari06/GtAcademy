using GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin;
using GtAcademy.Application.Admin.Topics.Commands.CreateTopic;
using GtAcademy.Application.Admin.Topics.Commands.DeleteTopic;
using GtAcademy.Application.Admin.Topics.Commands.EditTopic;
using GtAcademy.Application.Admin.Topics.Queries.GetCoursesTopics;
using GtAcademy.Application.Admin.Topics.Queries.GetTopicForEdit;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Web.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 2")]
    public class TopicsController : Controller
    {
        private readonly IMediator _mediator;

        public TopicsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Admin/Topics/{id}")]
        public async Task<IActionResult> Index(Guid id)
        {
            var result = await _mediator.Send(new GetCoursesTopicsQuery(id));

            if (result.IsError) return NotFound();

            ViewBag.CourseId = id;
            return View(result.Value);
        }

        [Route("Admin/Topics/Create/{id}")]
        public IActionResult Create(Guid id)
        {
            ViewBag.CourseId = id;
            return View();
        }

        [HttpPost("Admin/Topics/Create")]
        public async Task<IActionResult> Create(CreateTopicDto topicDto)
        {
            var result = await _mediator.Send(new CreateTopicCommand(topicDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    ModelState.Clear();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    ViewBag.CourseId = topicDto.CourseId;
                    return View(topicDto);
                }

                return NotFound();
            }

            return RedirectToAction(nameof(Index), new { id = topicDto.CourseId });
        }

        [Route("Admin/Topics/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _mediator.Send(new GetTopicForEditQuery(id));

            if (result.IsError) return NotFound();

            return View(result.Value);
        }

        [HttpPost("Admin/Topics/Edit")]
        public async Task<IActionResult> Edit(EditTopicDto topicDto)
        {
            var result = await _mediator.Send(new EditTopicCommand(topicDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    ModelState.Clear();
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return View(topicDto);
                }

                return NotFound();
            }

            return RedirectToAction(nameof(Index), new { id = topicDto.CourseId });
        }

        [HttpPost("Admin/Topics/Delete")]
        public async Task<IActionResult> Delete(int topicId, Guid courseId)
        {
            var result = await _mediator.Send(new DeleteTopicCommand(topicId));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    var indexResult = await _mediator.Send(new GetCoursesTopicsQuery(courseId));

                    if (indexResult.IsError) return NotFound();

                    ViewBag.CourseId = courseId;
                    ViewBag.ValidationError = result.FirstError.Description;

                    return View("Index", indexResult.Value);
                }

                return NotFound();
            }

            return RedirectToAction(nameof(Index), new { id = result.Value });
        }
    }
}
