var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
//app.UseStatusCodePages("text/plain", "Error: Resource Not Found. Status code: {0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{page?}",
    defaults: new { controller = "Home", action = "Main" });

app.MapControllerRoute(
    name: "customer",
    pattern: "my/{page?}",
    defaults: new { controller = "Customer", action = "Main" });

app.Run();
