using AutoMapper;
using HomeZone.Core.Mapping;
using HomeZone.Data.Models;
using System;

namespace HomeZone.Services.Admin.Models.Logs
{
    public class LogsListingServiceModel : IMapFrom<Log>, IHaveCustomMapping
    {
        public string UserName { get; set; }

        public string OperationType { get; set; }

        public string ModifiedTable { get; set; }

        public DateTime ModifiedDate { get; set; }

        public void ConfigureMapping(Profile mapper)
        {
            mapper.CreateMap<Log, LogsListingServiceModel>()
                .ForMember(l => l.OperationType, cfg => cfg.MapFrom(l => l.LogOperation))
                .ForMember(l => l.ModifiedTable, cfg => cfg.MapFrom(l => l.ModifiedTable))
                .ForMember(l => l.ModifiedDate, cfg => cfg.MapFrom(l => l.ModifiedOn));
        }
    }
}