using RFMoneyMatters.CurrentUser.Abstraction;

namespace RFMoneyMatters.CurrentUser
{
    public static class Extension
    {
        public static IServiceCollection AddCurrentUser(this IServiceCollection service)
        {
            service.AddScoped<ICurrentUser, CurrentUser>();
            service.AddScoped<CurrentUserMiddleware>();
            return service;
        }

        public static IApplicationBuilder AddCurrentUserMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<CurrentUserMiddleware>();
            return app;
        }
    }

}