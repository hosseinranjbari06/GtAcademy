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
        private IPermissionService _permissionService;

        private readonly List<int> _roleIds = [];

        public CheckPermissionAttribute(string roleIds)
        {
            foreach (string roleId in roleIds.Split(" "))
            {
                _roleIds.Add(Convert.ToInt32(roleId));
            }
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity != null &&
                context.HttpContext.User.Identity.IsAuthenticated)
            {
                _permissionService = (IPermissionService)context.HttpContext.RequestServices.GetService(typeof(IPermissionService))!;
                Guid userId = Guid.Parse(context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

                bool isVerified = false;

                foreach (int id in _roleIds)
                {                
                    if (_permissionService.UserHasRole(userId, id).Result)
                    {
                        isVerified = true;
                        break;
                    }
                }

                //_permissionService.DisposeContext();

                if (!isVerified)
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
