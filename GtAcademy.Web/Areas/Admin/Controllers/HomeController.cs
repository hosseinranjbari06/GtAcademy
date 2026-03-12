using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Web.Security;
using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 2 3 4")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
