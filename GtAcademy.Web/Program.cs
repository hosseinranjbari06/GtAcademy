using GtAcademy.Application;
using GtAcademy.Infrastructure;
using GtAcademy.Infrastructure.Common.Persistence;
using GtAcademy.Infrastructure.Tools.Persistence.SmsSender;
using GtAcademy.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(30);
        options.AccessDeniedPath = "/AccessDenied";
    });

builder.Services.Configure<SmsSenderSettings>(
    builder.Configuration.GetSection("SmsSenderSettings"));

builder.Services.AddInfrastructre(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
  name: "admin",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
