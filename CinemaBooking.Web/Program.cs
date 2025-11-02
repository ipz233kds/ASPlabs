using CinemaBooking.Shared.DTOs;
using CinemaBooking.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace CinemaBooking.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddHttpClient("MyApi", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7102/");
            });

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddSingleton<LoginDto>();

            await builder.Build().RunAsync();
        }
    }
}