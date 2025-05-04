using RFMoneyMatters.CurrentUser.Abstraction;
using RFMoneyMatters.CurrentUser.ExtensionMethods;
using System.Globalization;

namespace RFMoneyMatters.CurrentUser
{
    public class CurrentUserMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var service = context.RequestServices.GetRequiredService<ICurrentUser>();
            service.Id = context.User.GetUserId();
            service.Email = context.User.GetUserEmail();
            service.Roles = context.User.GetUserRoles();

            await next.Invoke(context);
        }
    }
}
