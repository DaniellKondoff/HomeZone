using HomeZone.Services.Models.Reservation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface IReservetionService
    {
        Task<bool> IsReservedAsync(int propertyId, DateTime startDate, DateTime endDate);

        Task<bool> MakeReservationsAsync(int propertyId, DateTime startDate, DateTime endDate, string userId);

        Task<IEnumerable<ReservationsListingServiceViewModel>> ReservationsByUserIdAsync(string userId);

        Task<bool> DeleteAsync(int id);
    }
}
