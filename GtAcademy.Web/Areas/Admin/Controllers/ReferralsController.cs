using GtAcademy.Application.Admin.ReferralOptions.Commands.EditRewardPercent;
using GtAcademy.Application.Admin.ReferralOptions.Queries.GetReferralOptions;
using GtAcademy.Web.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1")]
    public class ReferralsController : Controller
    {
        private readonly IMediator _mediator;

        public ReferralsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> ReferralOptions()
        {
            var result = await _mediator.Send(new GetReferralOptionsQuery());

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> EditOptions(float rewardPercent)
        {
            var result = await _mediator.Send(new EditRewardPercentCommand(rewardPercent));

            if (result.IsError)
            {
                ViewBag.Validation = result.FirstError.Description;
            }

            var percent = await _mediator.Send(new GetReferralOptionsQuery());

            return View("ReferralOptions", percent);
        }
    }
}
