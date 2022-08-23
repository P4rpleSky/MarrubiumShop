using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => options.LoginPath = "/login");
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();  

app.UseStaticFiles();

app.MapControllerRoute(
    name: "products_catalog",
    pattern: "catalog/{id?}",
    defaults: new { controller = "Catalog", action = "Main" });

app.MapControllerRoute(
    name: "customer",
    pattern: "my/{page?}",
    defaults: new { controller = "Customer", action = "Main" });

app.MapControllerRoute(
    name: "sign-in",
    pattern: "login",
    defaults: new { controller = "Customer", action = "SignIn" });

app.MapControllerRoute(
    name: "sign-up",
    pattern: "registration",
    defaults: new { controller = "Customer", action = "SignUp" });

app.MapControllerRoute(
    name: "default",
    pattern: "{page?}",
    defaults: new { controller = "Home", action = "Main" });

app.Run();
