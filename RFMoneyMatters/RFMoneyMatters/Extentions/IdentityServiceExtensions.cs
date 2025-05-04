using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.Models;
using RFMoneyMatters.Services;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace RFMoneyMatters.Extentions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            var key = Encoding.ASCII.GetBytes(config["TokenKey"]);
            services.AddIdentityCore<Person>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<RaiDbContext>()
            .AddSignInManager<SignInManager<Person>>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                     .AddCookie(IdentityConstants.ApplicationScheme, opts =>
                     {
                         opts.Cookie.Name = IdentityConstants.ApplicationScheme;
                         opts.Cookie.HttpOnly = true;
                         opts.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                     })
       .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        // Ensure we always have an error and error description.
                        if (string.IsNullOrEmpty(context.Error))
                            context.Error = "invalid_token";

                        if (string.IsNullOrEmpty(context.ErrorDescription))
                            context.ErrorDescription = "This request requires a valid JWT access token to be provided";

                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            Converters = { new JsonStringEnumConverter() },
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };

                        return context.Response.WriteAsJsonAsync("errror", jsonSerializerOptions);
                    },
                    OnForbidden = context =>
                    {
                        var jsonSerializerOptions = new JsonSerializerOptions()
                        {
                            Converters = { new JsonStringEnumConverter() },
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                        };
                        return context.Response.WriteAsJsonAsync("error", jsonSerializerOptions);
                    }

                };
            });


            services.AddScoped<TokenService>();

            return services;
        }
    }
}