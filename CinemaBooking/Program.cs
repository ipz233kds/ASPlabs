using Microsoft.EntityFrameworkCore;
using CinemaBooking.Data.Models;
using CinemaBooking.Data.Infrastructure;
using Microsoft.AspNetCore.Identity;

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

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration["ConnectionStrings:IdentityConnection"])
);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {
    opts.User.RequireUniqueEmail = true;
    opts.Password.RequiredLength = 8;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = true;
    opts.Password.RequireUppercase = true;
    opts.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<AppIdentityDbContext>();


var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

await IdentitySeedData.EnsurePopulatedAsync(app.Services);
SeedData.EnsurePopulated(app.Services);

app.Run();