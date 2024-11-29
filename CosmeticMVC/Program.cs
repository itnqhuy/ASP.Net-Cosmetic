using CosmeticMVC.Data;
using CosmeticMVC.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CosmeticContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"));
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add authentication with cookie
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "CustomerCookie";
    options.DefaultChallengeScheme = "StaffCookie";
})
    .AddCookie("StaffCookie", options =>
    {
        options.LoginPath = "/Staff/Login";
        options.LogoutPath = "/Staff/LogOut";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(7);
    })
    .AddCookie("CustomerCookie", options =>
    {
        options.LoginPath = "/Customer/Login"; // Customer login path
        options.LogoutPath = "/Customer/LogOut";
        options.AccessDeniedPath = "/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(7);
    });

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  // Serve static files from wwwroot

app.UseRouting();
app.UseSession();

app.UseAuthentication();  // Ensure this comes before UseAuthorization
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();