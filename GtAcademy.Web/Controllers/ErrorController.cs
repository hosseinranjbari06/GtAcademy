using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{code}")]
        public IActionResult Error(int code)
        {
            return code switch
            {
                404 => View("NotFound"),
                _ => BadRequest()
            };
        }
    }
}
