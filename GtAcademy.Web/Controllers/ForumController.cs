using GtAcademy.Application.Admin.Roles.Queries.GetRolesForAdmin;
using GtAcademy.Application.Forum.Commands.AnswerQuestion;
using GtAcademy.Application.Forum.Commands.CloseQuestion;
using GtAcademy.Application.Forum.Commands.CreateQuestion;
using GtAcademy.Application.Forum.Commands.DeleteQuestion;
using GtAcademy.Application.Forum.Queries.CanUserChangeQuestionStatus;
using GtAcademy.Application.Forum.Queries.GetForumQuestionDetails;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsCount;
using GtAcademy.Application.Forum.Queries.GetForumQuestionsList;
using GtAcademy.Domain.Courses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GtAcademy.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IMediator _mediator;

        public ForumController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("/Forum")]
        public async Task<IActionResult> Index(ForumQuestionsSearchDto search)
        {
            var questions = await _mediator.Send(new GetForumQuestionsListQuery(search));
            var questionsCount = await _mediator.Send(new GetForumQuestionsCountQuery());

            ViewBag.Search = search;
            ViewBag.PagesCount = Math.Ceiling((float)questionsCount / (float)search.Take);

            return View(questions);
        }

        [Authorize]
        [Route("/Forum/CreateQuestion/{courseId}")]
        public IActionResult CreateQuestion(Guid courseId)
        {
            ViewBag.CourseId = courseId;
            return View();
        }

        [Authorize]
        [HttpPost("/Forum/CreateQuestion/{courseId}")]
        public async Task<IActionResult> CreateQuestion(CreateQuestionDto questionDto)
        {
            questionDto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _mediator.Send(new CreateQuestionCommand(questionDto));

            if (result.IsError)
            {
                if (result.FirstError.Type != ErrorOr.ErrorType.Validation)
                {
                    return NotFound();
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    ViewBag.CourseId = questionDto.CourseId;

                    return View(questionDto);
                }
            }

            if (questionDto.IsTicket) return Redirect("/ShowTicket/" + result.Value);

            return RedirectToAction("ShowQuestion", new { questionId = result.Value });
        }

        [Route("/Forum/ShowQuestion/{questionId}")]
        public async Task<IActionResult> ShowQuestion(Guid questionId)
        {
            var result = await _mediator.Send(new GetForumQuestionDetailsQuery(questionId));

            if (result.IsError) return NotFound();

            if (User.Identity.IsAuthenticated == true)
            {
                var canUserChangeQuestionStatus = await _mediator.Send(new CanUserChangeQuestionStatusQuery(questionId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

                if (canUserChangeQuestionStatus.IsError) return NotFound();

                ViewBag.CanUserChangeQuestionStatus = canUserChangeQuestionStatus.Value;
            }
            else
            {
                ViewBag.CanUserChangeQuestionStatus = false;
            }

            return View(result.Value);
        }

        [Authorize]
        [HttpPost("/Forum/AnswerQuestion")]
        public async Task<IActionResult> AnswerQuestion(CreateAnswerDto answerDto)
        {
            answerDto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var result = await _mediator.Send(new AnswerQuestionCommand(answerDto));

            if (result.IsError)
            {
                if (result.FirstError.Type == ErrorOr.ErrorType.Validation)
                {
                    var questionResult = await _mediator.Send(new GetForumQuestionDetailsQuery(answerDto.QuestionId));
                    if (questionResult.IsError) return NotFound();

                    ViewBag.ValidationError = result.FirstError.Description;
                    ViewBag.ContentValue = answerDto.Content;

                    return View("ShowQuestion", questionResult.Value);
                }

                return NotFound();
            }

            return RedirectToAction(nameof(ShowQuestion), new { questionId = answerDto.QuestionId });
        }

        [Authorize]
        [HttpPost("/Forum/DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            var result = await _mediator.Send(new DeleteQuestionCommand(questionId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        [HttpPost("/Forum/CloseQuestion")]
        public async Task<IActionResult> CloseQuestion(Guid questionId)
        {
            var result = await _mediator.Send(new CloseQuestionCommand(questionId, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            return RedirectToAction(nameof(ShowQuestion), new { questionId = questionId });
        }
    }
}
