using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using HomeZone.Services.Models.Reservation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var reserve = await this.db.Reservetions.FindAsync(id);

            if (reserve == null)
            {
                return false;
            }

            this.db.Reservetions.Remove(reserve);
            await this.db.SaveChangesAsync();

            return true;
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

        public async Task<bool> MakeReservationsAsync(int propertyId, DateTime startDate, DateTime endDate, string userId)
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
                EndDate = endDate,
                UserId = userId
            };

            await this.db.Reservetions.AddAsync(reservetion);
            await this.db.SaveChangesAsync();
            success = true;

            return success;
        }

        public async Task<IEnumerable<ReservationsListingServiceViewModel>> ReservationsByUserIdAsync(string userId)
        {
            return await this.db.Reservetions
                .Where(r => r.UserId == userId)
                .ProjectTo<ReservationsListingServiceViewModel>()
                .ToListAsync();
        }
    }
}