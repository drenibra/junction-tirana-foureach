using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using RFMoneyMatters.Configurations;
using RFMoneyMatters.Controllers;
using RFMoneyMatters.Extentions;
using RFMoneyMatters.Interface;
using RFMoneyMatters.Implementation.Interfaces;
using RFMoneyMatters.Implementation.Services;
using RFMoneyMatters.Models;
using RFMoneyMatters.Services;
using System.Text;
using RFMoneyMatters.CurrentUser;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<RaiDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(opt => {
    opt.AddPolicy("CorsPolicy", policy => {
        policy.AllowAnyMethod().AllowCredentials().AllowAnyHeader().WithOrigins("http://localhost:3000");
    });
});
builder.Services.AddIdentityCore<Person>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RaiDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<AuthController>();


builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<ILessonQuizService, LessonQuizService>();
builder.Services.AddScoped<ILessonQuizResultService, LessonQuizResultService>();
builder.Services.AddScoped<IUserChallengeService, UserChallengeService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCurrentUser();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.AddCurrentUserMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
