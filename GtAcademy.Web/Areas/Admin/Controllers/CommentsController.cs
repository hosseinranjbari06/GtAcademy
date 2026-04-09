using GtAcademy.Application.Admin.CourseComments.Commands.DeleteCourseCommentByAdmin;
using GtAcademy.Application.Admin.CourseComments.Commands.SubmitCourseCommentByAdmin;
using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentDetailsForAdmin;
using GtAcademy.Application.Admin.CourseComments.Queries.GetCourseCommentsListForAdmin;
using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Web.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 4")]
    public class CommentsController : Controller
    {
        private readonly IMediator _mediator;

        private readonly ICourseCommentService _courseCommentService;

        public CommentsController(IMediator mediator, ICourseCommentService courseCommentService)
        {
            _mediator = mediator;
            _courseCommentService = courseCommentService;
        }

        public async Task<IActionResult> Index(SearchCourseCommentsListDto searchDto)
        {
            var comments = await _mediator.Send(new GetCourseCommentsListForAdminQuery(searchDto));

            return View(comments);
        }

        public async Task<IActionResult> SubmitComment(Guid id)
        {
            var result = await _mediator.Send(new SubmitCourseCommentByAdminCommand(id));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [ActionName("Delete")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var result = await _mediator.Send(new DeleteCourseCommentByAdminCommand(id));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _mediator.Send(new GetCourseCommentDetailsForAdminQuery(id));

            if (result.IsError) return NotFound();

            return View(result.Value);
        }
    }
}
