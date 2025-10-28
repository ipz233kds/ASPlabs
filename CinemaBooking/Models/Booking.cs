using System.Collections.Generic;
using System.Linq;

namespace CinemaBooking.Models
{
    public class Booking
    {
        public List<BookingLine> Lines { get; set; } = new List<BookingLine>();

        public virtual void AddItem(MovieSession session, int quantity)
        {
            BookingLine? line = Lines
                .Where(p => p.MovieSession.MovieSessionID == session.MovieSessionID)
                .FirstOrDefault();

            if (line == null)
            {
                Lines.Add(new BookingLine
                {
                    MovieSession = session,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(MovieSession session) =>
            Lines.RemoveAll(l => l.MovieSession.MovieSessionID == session.MovieSessionID);

        public virtual decimal ComputeTotalValue() =>
            Lines.Sum(e => e.MovieSession.Price * e.Quantity);
        public virtual void Clear() => Lines.Clear();
    }
}