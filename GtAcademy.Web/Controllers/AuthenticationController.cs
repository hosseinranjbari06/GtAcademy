using GtAcademy.Application.Authentication.Commands.RegisterWithPhone;
using GtAcademy.Application.Authentication.Commands.VerifyPhoneNumber;
using GtAcademy.Application.Authentication.Common;
using GtAcademy.Application.Authentication.Queries.LoginWithPhone;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GtAcademy.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(string userName, string phoneNumber)
        {
            var result = await _mediator.Send(new RegisterWithPhoneCommand(userName, phoneNumber));

            if (result.IsError)
            {
                ModelState.AddModelError(result.FirstError.Code, result.FirstError.Description);
                return View(userName, phoneNumber);
            }

            return RedirectToAction("VerifyPhoneNumber", phoneNumber);
        }

        public IActionResult VerifyPhoneNumber(string phoneNumber)
        {
            return View(phoneNumber);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber, string code)
        {
            var result = await _mediator.Send(new VerifyPhoneNumberCommand(phoneNumber, code));

            if (result.IsError)
            {
                ModelState.AddModelError(result.FirstError.Code, result.FirstError.Description);
                return View(phoneNumber, code);
            }

            await SignInUser(result.Value);

            return RedirectToAction("Index", "Home");
        }

        [Route("Login")]
        public IActionResult Login()
        {
            if(User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string phoneNumber, string code)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            var result = await _mediator.Send(new LoginWithPhoneQuery(phoneNumber));

            if (result.IsError)
            {
                ModelState.AddModelError(result.FirstError.Code, result.FirstError.Description);
                return View(phoneNumber, code);
            }

            return RedirectToAction("VerifyPhoneNumber", phoneNumber);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> SignOutUser()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(AuthenticationResult result, bool isPersistent = true)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString()),
                new Claim(ClaimTypes.MobilePhone, result.PhoneNumber),
                new Claim(ClaimTypes.Name, result.UserName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var properties = new AuthenticationProperties()
            {
                IsPersistent = isPersistent
            };

            await HttpContext.SignInAsync(principal, properties);
        }
    }
}
