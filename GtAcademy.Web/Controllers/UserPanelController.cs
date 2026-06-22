using GtAcademy.Application.Referrals.Queries.GetUserReferralInfo;
using GtAcademy.Application.Users.Commands.EditUserProfile;
using GtAcademy.Application.Users.Common;
using GtAcademy.Application.Users.Queries.GetUserAvatarNameById;
using GtAcademy.Application.Users.Queries.GetUserProfile;
using GtAcademy.Application.Users.Queries.GetUserProfileForEdit;
using GtAcademy.Web.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GtAcademy.Web.Controllers
{
    [Authorize]
    public class UserPanelController : Controller
    {
        private readonly IMediator _mediator;

        public UserPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _mediator.Send(new GetUserProfileQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return BadRequest();

            return View(result.Value);
        }

        [Route("EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var result = await _mediator.Send(new GetUserProfileForEditQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return BadRequest();

            return View(result.Value);
        }

        [HttpPost("EditProfile")]
        public async Task<IActionResult> EditProfile(EditUserProfileDto profileDto, IFormFile? avatarFile)
        {
            string oldAvatarName = "";
            profileDto.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            if (avatarFile != null)
            {
                var fileValidation = FileManager.IsFileValid(avatarFile);

                if (fileValidation.IsError)
                {
                    ModelState.AddModelError("AvatarName", fileValidation.FirstError.Description);
                    return View(profileDto);
                }

                var avatarResult = await _mediator.Send(new GetUserAvatarNameByIdQuery(profileDto.UserId));
                if (!avatarResult.IsError) { oldAvatarName = avatarResult.Value; }

                profileDto.AvatarName = FileManager.GenerateRandomFileName(avatarFile.FileName);
            }

            var result = await _mediator.Send(new EditUserProfileCommand(profileDto));

            if (result.IsError)
            {
                ModelState.AddModelError(result.FirstError.Code, result.FirstError.Description);
            }
            else
            {
                if (avatarFile != null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\img\\users");

                    if (!string.IsNullOrEmpty(oldAvatarName) && oldAvatarName != "default.jpg")
                    {
                        await FileManager.DeleteFile(path, oldAvatarName);
                    }

                    await FileManager.SaveFile(avatarFile, path, profileDto.AvatarName!);
                }

                ViewBag.IsSuccess = true;
            }

            return View(profileDto);
        }

        [Route("Networking")]
        public async Task<IActionResult> Networking()
        {
            var result = await _mediator.Send(new GetUserReferralInfoQuery(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!)));

            if (result.IsError) return NotFound();

            ViewBag.ReferrerLink = Url.Action("Register", "Authentication", new { referrerCode = result.Value.ReferralCode }, "https", HttpContext.Request.Host.Value);

            return View(result.Value);
        }
    }
}
