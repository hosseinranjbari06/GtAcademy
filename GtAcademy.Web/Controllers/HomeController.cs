using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hi");
        }
    }
}
