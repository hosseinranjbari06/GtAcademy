using GtAcademy.Application.Wallets.Queries.GetUsersWalletWithDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GtAcademy.Web.Controllers
{
    public class WalletController : Controller
    {
        private readonly IMediator _mediator;

        public WalletController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var wallerDto = await _mediator.Send(new GetUsersWalletWithDetailsQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (wallerDto.IsError) return NotFound();

            return View(wallerDto.Value);
        }
    }
}
