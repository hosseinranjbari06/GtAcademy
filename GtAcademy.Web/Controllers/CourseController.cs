using GtAcademy.Application.Courses.Commands.CreateComment;
using GtAcademy.Application.Courses.Commands.CreateCourseComment;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Courses.Queries.GetCourseDetails;
using GtAcademy.Application.Courses.Queries.GetCoursesList;
using GtAcademy.Application.Courses.Queries.HasUserPermissionToEpisode;
using GtAcademy.Application.Orders.Commands.AddCourseToOrder;
using GtAcademy.Application.Orders.Commands.DeleteCourseFromOrder;
using GtAcademy.Domain.Courses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GtAcademy.Web.Controllers
{
    public class CourseController : Controller
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/Courses")]
        public async Task<IActionResult> GetCourses(string search = "", int seperate = 6, int pageId = 1)
        {
            var courses = await _mediator.Send(new GetCoursesListQuery(search, seperate, pageId));
            return View(courses);
        }

        [Route("/CourseDetails/{courseId}")]
        public async Task<IActionResult> CourseDetails(Guid courseId)
        {
            var result = await _mediator.Send(new GetCourseDetailsQuery(courseId));

            if (result.IsError)
                return NotFound();

            return View(result.Value);
        }

        [Authorize]
        [Route("/DownloadEpisodeFile/{episodeId}")]
        public async Task<IActionResult> DownloadEpisodeFile(Guid episodeId)
        {
            var result = await _mediator.Send(new HasUserPermissionToEpisodeQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), episodeId));

            if (result.IsError) return NotFound();

            string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/coursefiles", result.Value);
            byte[] file = System.IO.File.ReadAllBytes(filepath);
            return File(file, "application/force-download", result.Value);
        }

        [Authorize]
        [HttpPost("/AddComment")]
        public async Task<IActionResult> AddComment(Guid courseId, string content)
        {
            var comment = new CreateCourseCommentDto()
            {
                CourseId = courseId,
                UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!),
                Content = content
            };

            var result = await _mediator.Send(new CreateCourseCommentCommand(comment));

            if (result.IsError) return NotFound();

            return RedirectToAction("CourseDetails", new { courseId});
        }
    }
}
