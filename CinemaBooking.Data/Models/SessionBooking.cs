using System.Text.Json.Serialization;
using CinemaBooking.Data.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaBooking.Data.Models
{
    public class SessionBooking : Booking
    {

        public static Booking GetBooking(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()
                .HttpContext?.Session;

            SessionBooking? booking = session?.GetJson<SessionBooking>("Booking")
                ?? new SessionBooking();

            booking.Session = session;
            return booking;
        }

        [JsonIgnore]
        public ISession? Session { get; set; }

        public override void AddItem(MovieSession session, int quantity)
        {
            base.AddItem(session, quantity);
            Session?.SetJson("Booking", this);
        }

        public override void RemoveLine(MovieSession session)
        {
            base.RemoveLine(session);
            Session?.SetJson("Booking", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session?.Remove("Booking");
        }
    }
}