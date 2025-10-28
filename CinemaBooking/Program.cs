using Microsoft.EntityFrameworkCore;
using CinemaBooking.Models;
using CinemaBooking.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CinemaDbContext>(opts => {
    opts.UseSqlServer(
        builder.Configuration["ConnectionStrings:CinemaBookingConnection"]);
});

builder.Services.AddScoped<ICinemaRepository, EFCinemaRepository>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<Booking>(sp => SessionBooking.GetBooking(sp));


var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();