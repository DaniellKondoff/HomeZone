using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Services.Contracts;
using HomeZone.Services.Models.Location;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly HomeZoneDbContext db;

        public LocationService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ListingBasicCitiesServiceModels>> GetAllBasicCities()
        {
            return await this.db.Cities
                .OrderBy(c => c.Id)
                .ProjectTo<ListingBasicCitiesServiceModels>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ListingBasicSectionServiceModel>> GetAllSectionByCity(int cityId)
        {
            return await this.db.Sections
                .Where(s => s.CityId == cityId)
                .OrderBy(s => s.Name)
                .ProjectTo<ListingBasicSectionServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<ListingBasicSectionServiceModel>> GetAllSectionByDefault()
        {
            var city = await this.db.Cities.FirstAsync();

            return await this.db.Sections
                .Where(s => s.CityId == city.Id)
                .OrderBy(s => s.Name)
                .ProjectTo<ListingBasicSectionServiceModel>()
                .ToListAsync();
        }
    }
}
