﻿using HomeZone.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace HomeZone.Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection service)
        {
            Assembly
                 .GetAssembly(typeof(IService))
                 .GetTypes()
                 .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                 .Select(t => new
                 {
                     Interface = t.GetInterface($"I{t.Name}"),
                     Implementation = t
                 })
                 .ToList()
                 .ForEach(s => service.AddTransient(s.Interface, s.Implementation));

            return service;
        }
    }
}
