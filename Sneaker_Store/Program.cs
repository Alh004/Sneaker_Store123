using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sneaker_Store.Model;
using Sneaker_Store.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add HTTP context accessor for accessing the current HttpContext in services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Register the repository implementations
builder.Services.AddSingleton<IKundeRepository, DB_Kunde>();
builder.Services.AddSingleton<ISkoRepository, DB_Sko>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// Register Kurv with scoped lifetime
builder.Services.AddScoped<Kurv>();

// Add session services
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Den logger brugere ud efter 30 min og siden vil blive nulstillet
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Til session
app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();