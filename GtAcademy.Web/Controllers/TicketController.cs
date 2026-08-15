using GtAcademy.Application.Forum.Commands.AnswerQuestion;
using GtAcademy.Application.Forum.Commands.CloseQuestion;
using GtAcademy.Application.Forum.Commands.DeleteQuestion;
using GtAcademy.Application.Forum.Queries.CanUserChangeQuestionStatus;
using GtAcademy.Application.Forum.Queries.GetForumQuestionDetails;
using GtAcademy.Application.Tickets.Commands.AnswerTicket;
using GtAcademy.Application.Tickets.Queries.GetTicketDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GtAcademy.Web.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/ShowTicket/{ticketId}")]
        public async Task<IActionResult> ShowTicket(Guid ticketId)
        {
            var canUserChangeQuestionStatus = await _mediator.Send(new CanUserChangeQuestionStatusQuery(ticketId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (canUserChangeQuestionStatus.IsError || !canUserChangeQuestionStatus.Value) return NotFound();

            var result = await _mediator.Send(new GetTicketDetailsQuery(ticketId));

            if (result.IsError) return NotFound();

            ViewBag.CanUserChangeQuestionStatus = canUserChangeQuestionStatus.Value;

            return View(result.Value);
        }

        [HttpPost("/Ticket/AnswerTicket")]
        public async Task<IActionResult> AnswerTicket(CreateAnswerDto answerDto)
        {
            var canUserChangeQuestionStatus = await _mediator.Send(new CanUserChangeQuestionStatusQuery(answerDto.QuestionId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (canUserChangeQuestionStatus.IsError || !canUserChangeQuestionStatus.Value) return NotFound();

            answerDto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new AnswerTicketCommand(answerDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    var questionResult = await _mediator.Send(new GetTicketDetailsQuery(answerDto.QuestionId));
                    if (questionResult.IsError) return NotFound();

                    ViewBag.ValidationError = result.FirstError.Description;
                    ViewBag.ContentValue = answerDto.Content;

                    return View("ShowTicket", questionResult.Value);
                }

                return NotFound();
            }

            return RedirectToAction(nameof(ShowTicket), new { ticketId = answerDto.QuestionId });
        }

        [HttpPost("/Ticket/DeleteTicket")]
        public async Task<IActionResult> DeleteTicket(Guid ticketId)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand(ticketId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            return Redirect("/MyTickets");
        }

        [HttpPost("/Ticket/CloseTicket")]
        public async Task<IActionResult> CloseTicket(Guid ticketId)
        {
            var result = await _mediator.Send(new CloseQuestionCommand(ticketId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(ShowTicket), new { ticketId = ticketId });
        }
    }
}
