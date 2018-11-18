using AutoMapper;
using HomeZone.Core.Mapping;
using System;

namespace HomeZone.Services.Models.Reservation
{
    public class ReservationsListingServiceViewModel : IMapFrom<Data.Models.Reservation>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Title { get; set; }

        public string CityName { get; set; }

        public string SectionName { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Data.Models.Reservation, ReservationsListingServiceViewModel>()
                .ForMember(r => r.Title, cfg => cfg.MapFrom(r => r.Property.Title))
                .ForMember(r => r.CityName, cfg => cfg.MapFrom(r => r.Property.City.Name))
                .ForMember(r => r.SectionName, cfg => cfg.MapFrom(r => r.Property.Section.Name));
        }
    }
}
