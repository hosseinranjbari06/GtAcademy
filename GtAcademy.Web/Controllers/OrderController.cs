using AutoMapper;
using GtAcademy.Application.Orders.Commands.AddCourseToOrder;
using GtAcademy.Application.Orders.Commands.DeleteCourseFromOrder;
using GtAcademy.Application.Orders.Commands.OrderPayment;
using GtAcademy.Application.Orders.Queries.GetUserCurrentOrder;
using GtAcademy.Application.Wallets.Queries.GetUsersWalletBalance;
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

        [Authorize]
        [Route("/Order")]
        public async Task<IActionResult> GetCurrentOrder()
        {
            var result = await _mediator
                .Send(new GetUserCurrentOrderQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            var walletBalance = await _mediator.Send(new GetUsersWalletBalanceQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError)
            {
                ViewBag.IsEmpty = true;
                return View();
            }

            ViewBag.WalletBalance = walletBalance.Value;
            ViewBag.IsPaymentAllowed = walletBalance.Value >= result.Value.TotalAmount;

            return View(result.Value);
        }

        [Authorize]
        public async Task<IActionResult> AddCourseToOrder(Guid courseId)
        {
            var result = await _mediator
                .Send(new AddCourseToOrderCommand(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), courseId));

            if (result.IsError)
                return NotFound();

            return RedirectToAction("GetCurrentOrder");
        }

        [Authorize]
        public async Task<IActionResult> DeleteCourseFromOrder(Guid courseId)
        {
            var result = await _mediator
                .Send(new DeleteCourseFromOrderCommand(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!), courseId));

            if (result.IsError)
                return NotFound();

            return RedirectToAction("GetCurrentOrder");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> OrderPayment()
        {
            var result = await _mediator.Send(new OrderPaymentCommand(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(GetCurrentOrder));
        }
    }
}
