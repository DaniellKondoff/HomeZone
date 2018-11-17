using System;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface IReservetionService
    {
        Task<bool> IsReservedAsync(int propertyId, DateTime startDate, DateTime endDate);

        Task<bool> MakeReservationsAsync(int propertyId, DateTime startDate, DateTime endDate);
    }
}
