using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.Models;
using RFMoneyMatters.Services;
using System.Text;

namespace RFMoneyMatters.Extentions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<Person>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<RaiDbContext>()
            .AddSignInManager<SignInManager<Person>>();

            // bind JWT settings, etc...

            services.AddAuthentication(options =>
            {
                // we still use JWT for API auth…
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                // …but for SignInManager.SignOutAsync() we need this:
                options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;
            })
            // register the Identity.Application cookie
            .AddCookie(IdentityConstants.ApplicationScheme, opts =>
            {
                opts.Cookie.Name = IdentityConstants.ApplicationScheme;
                opts.Cookie.HttpOnly = true;
                opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            // then your JWT‐Bearer
            .AddJwtBearer(/* ... */);

            // no extra services.AddAuthentication() here
            services.AddScoped<TokenService>();
            return services;
        }

    }
}
