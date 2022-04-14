using AareonTechnicalTest.Models;
using AareonTechnicalTest.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace AareonTechnicalTest.Filters
{
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(PermissionItem item, PermissionAction action)
        : base(typeof(AuthorizeActionFilter))
        {
            Arguments = new object[] { item, action };
        }
    }

    public class AuthorizeActionFilter : IAuthorizationFilter
    {
        private readonly PermissionItem _item;
        private readonly PermissionAction _action;
        public AuthorizeActionFilter(PermissionItem item, PermissionAction action)
        {
            _item = item;
            _action = action;
        }
        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized =await AuthorizeRequest(context.HttpContext, _item, _action); 

            if (!isAuthorized)
            {
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
                return;
            }
        }

        private async Task<bool> AuthorizeRequest(HttpContext context, PermissionItem item, PermissionAction action)
        {
            if (item == PermissionItem.Note)
            {
                if (action==PermissionAction.Delete)
                {
                    var personId = Convert.ToInt32(context.Request.RouteValues["personId"]);
                    var personService = context.RequestServices.GetService<IPersonService>();     
                    return await personService.CanDeleteNote(personId);
                }
            }
            return true;
        }
    }
}

