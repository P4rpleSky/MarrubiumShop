var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<MarrubiumShop.Database.IDatabaseService, MarrubiumShop.Database.DatabaseService>();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer();
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
    name: "default",
    pattern: "{page?}",
    defaults: new { controller = "Home", action = "Main" });

app.MapControllerRoute(
    name: "customer",
    pattern: "my/{page?}",
    defaults: new { controller = "Customer", action = "Main" });

app.Run();
