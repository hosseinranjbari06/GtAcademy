using AutoMapper;
using GtAcademy.Application.Orders.Queries.GetUserCurrentOrder;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GtAcademy.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Order")]
        public async Task<IActionResult> GetCurrentOrder()
        {
            var result = await _mediator
                .Send(new GetUserCurrentOrderQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError)
            {
                ViewBag.IsEmpty = true;
                return View();
            }

            return View(result.Value);
        }
    }
}
