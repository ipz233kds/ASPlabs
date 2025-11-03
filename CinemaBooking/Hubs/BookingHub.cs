using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace CinemaBooking.Hubs
{
    public class BookingHub : Hub
    {
        public async Task LockSeat(long seatStatusId)
        {
            await Clients.Others.SendAsync("UpdateSeatToLocked", seatStatusId);
        }

        public async Task UnlockSeat(long seatStatusId)
        {
            await Clients.Others.SendAsync("UpdateSeatToAvailable", seatStatusId);
        }
    }
}