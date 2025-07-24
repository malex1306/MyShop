using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyShop.Domain.Interfaces;
using MyShop.Infrastructure.Customer;
using MyShop.Infrastructure.Persistence;
using MyShop.Infrastructure.Persistence.Repositories;
using MyShop.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Kestrel so konfigurieren, dass er HTTP und HTTPS Ports hört
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5290); // HTTP
    options.ListenLocalhost(7242, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS aktivieren
    });
});

// HTTPS-Redirect Middleware wissen lassen, auf welchen Port sie umleiten soll
builder.Services.Configure<Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionOptions>(options =>
{
    options.HttpsPort = 7242;
});

// DI-Registrierungen
builder.Services.AddScoped<IOrderRepository, EfCoreOrderRepository>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<IProductRepository, EfCoreProductRepository>();
builder.Services.AddScoped<ProductService>();

builder.Services.AddDbContext<MyShopDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<CustomerIdentity, IdentityRole<int>>()
    .AddEntityFrameworkStores<MyShopDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed Admin-Daten beim Start
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await Seed.SeedAdmin(services);
}

// HTTP Request Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
