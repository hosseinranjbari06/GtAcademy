using GtAcademy.Application.Common.Interfaces;
using GtAcademy.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace GtAcademy.Web.Security
{
    public class CheckPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private IRoleService _roleService;

        private readonly int _roleId = 0;

        public CheckPermissionAttribute(int roleId)
        {
            _roleId = roleId;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity != null &&
                context.HttpContext.User.Identity.IsAuthenticated)
            {
                _roleService = (IRoleService)context.HttpContext.RequestServices.GetService(typeof(IRoleService))!;
                Guid userId = Guid.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                if (!await _roleService.UserHasRole(userId, _roleId))
                {
                    context.Result = new RedirectResult("/NotFound");
                }
            }
            else
            {
                context.Result = new RedirectResult("/NotFound");
            }
        }
    }
}
