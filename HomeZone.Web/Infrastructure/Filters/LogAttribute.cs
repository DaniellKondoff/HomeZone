using HomeZone.Data.Enums;
using HomeZone.Services.Admin.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace HomeZone.Web.Infrastructure.Filters
{
    public class LogAttribute : ActionFilterAttribute
    {
        private Operation operationType;
        private string tableName;

        public LogAttribute(Operation operationtType, string tableName)
        {
            if (tableName == null)
            {
                throw new ArgumentNullException();
            }

            this.operationType = operationtType;
            this.tableName = tableName;
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userName = context.HttpContext.User.Identity.Name;
            IAdminLogService logService = (IAdminLogService)context.HttpContext.RequestServices.GetService(typeof(IAdminLogService));

            logService.Create(userName, operationType, tableName, DateTime.UtcNow);
        }
    }
}
