using Library.Common;
using Library.Services.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Library.API.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public string Roles { get; set; } = $"{GlobalConstants.NotConfirmedRoleName},{GlobalConstants.UserRoleName}";

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items[GlobalConstants.UserRoleName] as ResponseAuthDTO;

            if (user != null && user.isBlocked == true)
            {
                context.Result = new UnauthorizedObjectResult(user.Message);
            }
            if (user != null && user.Role == GlobalConstants.AdministratorRoleName)
            {

            }
            else if (user == null || !Roles.Contains(user.Role))
            {
                context.Result = new UnauthorizedObjectResult(GlobalConstants.NOT_AUTHORIZED);
            }

        }
    }
}
