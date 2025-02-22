using System.Security.Claims;
using Core.Extensions;
using DataAccess.Extensions;
using Front.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Front.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCore().AddDataAccess(builder.Configuration).AddFront();
builder.Services.AddHttpContextAccessor();

//подключаем аутентификацию со схемой Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => 
    { 
        options.LoginPath = "/auth/login"; 
        options.Cookie.Name = "authCookie";
    });
//подключаем серсив авторизации
builder.Services.AddAuthorization(options =>
{
    // Добавляем политику, которая проверяет наличие клайма с ролью "User"
    options.AddPolicy(Front.Constants.AuthorizePoliciesNames.UserPolicy, policy => 
        policy.RequireClaim(ClaimTypes.Role, UserRoles.User.ToString()));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

// Используем маршрутизацию
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); 
app.Run();