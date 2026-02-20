using GtAcademy.Application.Courses.Queries.GetAllStudentsCount;
using GtAcademy.Application.Courses.Queries.GetCourseCategories;
using GtAcademy.Application.Courses.Queries.GetCoursesCount;
using GtAcademy.Application.Courses.Queries.GetPopularCoursesList;
using GtAcademy.Application.Users.Queries.GetUsersCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GtAcademy.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CoursesCount = await _mediator.Send(new GetCoursesCountQuery());
            ViewBag.UsersCount = await _mediator.Send(new GetUsersCountQuery());
            ViewBag.StudentsCount = await _mediator.Send(new GetAllStudentsCountQuery());
            ViewBag.PopularCourses = await _mediator.Send(new GetPopularCoursesListQuery());
            ViewBag.CourseCategories = await _mediator.Send(new GetCourseCategoriesQuery());

            return View();
        }
    }
}
