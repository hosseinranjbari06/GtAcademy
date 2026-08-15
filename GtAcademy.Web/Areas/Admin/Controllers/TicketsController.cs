using GtAcademy.Application.Admin.Tickets.Queries.GetTicketsCount;
using GtAcademy.Application.Admin.Tickets.Queries.GetTicketsList;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using GtAcademy.Web.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtAcademy.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckPermission("1 2 3 4")]
    public class TicketsController : Controller
    {
        private readonly IMediator _mediator;

        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(ForumQuestionsSearchDto search)
        {
            var tickets = await _mediator.Send(new GetTicketsListQuery(search));
            var ticketsCount = await _mediator.Send(new GetTicketsCountQuery());

            ViewBag.Search = search;
            ViewBag.PagesCount = Math.Ceiling((float)ticketsCount / (float)search.Take);

            return View(tickets);
        }
    }
}
