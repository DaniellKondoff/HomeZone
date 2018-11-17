using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Services.Implementation
{
    public class ReservetionService : IReservetionService
    {
        private HomeZoneDbContext db;

        public ReservetionService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<bool> IsReservedAsync(int propertyId, DateTime startDate, DateTime endDate)
        {

            var reservation = await this.db.Reservetions.Where(r => r.PropertyId == propertyId).FirstOrDefaultAsync();

            if (reservation == null || startDate >= reservation.EndDate && endDate >= reservation.EndDate)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> MakeReservationsAsync(int propertyId, DateTime startDate, DateTime endDate)
        {
            bool success = false;

            var property = await this.db.Properties.FindAsync(propertyId);

            if (property == null)
            {
                success = false;
            }

            var reservetion = new Reservation
            {
                PropertyId = property.Id,
                StartDate = startDate,
                EndDate = endDate
            };

            await this.db.Reservetions.AddAsync(reservetion);
            await this.db.SaveChangesAsync();
            success = true;

            return success;
        }
    }
}