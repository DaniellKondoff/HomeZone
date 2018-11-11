﻿using AutoMapper;
using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Admin.Models.Properties
{
    public class AdminPropertyDetailsServiceModel : IMapFrom<Property>, IHaveCustomMapping
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsSold { get; set; }

        public bool IsForRent { get; set; }

        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }

        public int Space { get; set; }

        public string CityName { get; set; }

        public string SectionName { get; set; }

        public byte[] MainImage { get; set; }

        public byte[] SecondaryImage { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Property, AdminPropertyListinServiceModel>()
               .ForMember(p => p.CityName, cfg => cfg.MapFrom(p => p.City.Name))
               .ForMember(p => p.SectionName, cfg => cfg.MapFrom(p => p.Section.Name));
        }
    }
}