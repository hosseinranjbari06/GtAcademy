using GtAcademy.Application.Courses.Queries.GetCourseDetails;
using GtAcademy.Application.Courses.Queries.GetCoursesList;
using GtAcademy.Application.Orders.Commands.AddCourseToOrder;
using GtAcademy.Application.Orders.Commands.DeleteCourseFromOrder;
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

        public async Task<IActionResult> GetCourses(string search = "", int seperate = 6, int pageId = 1)
        {
            var courses = await _mediator.Send(new GetCoursesListQuery(search, seperate, pageId));
            return View(courses);
        }

        public async Task<IActionResult> CourseDetails(Guid courseId)
        {
            var result = await _mediator.Send(new GetCourseDetailsQuery(courseId));

            if (result.IsError)
                return NotFound();

            return View(result.Value);
        }

        [Authorize]
        public async Task<IActionResult> AddCourseToOrder(Guid courseId)
        {
            var result = await _mediator
                .Send(new AddCourseToOrderCommand(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), courseId));

            if (result.IsError)
                return NotFound();

            return RedirectToAction("GetCurrentOrder", "Order");
        }

        [Authorize]
        public async Task<IActionResult> DeleteCourseFromOrder(Guid courseId)
        {
            var result = await _mediator
                .Send(new DeleteCourseFromOrderCommand(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), courseId));

            if (result.IsError)
                return NotFound();

            return RedirectToAction("GetCurrentOrder", "Order");
        }
    }
}
