﻿using CodingAssessment.Application.Interfaces;
using CodingAssessment.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CodingAssessment.Application
{
    public static class ServiceCollectionConfiguration
    {
        /// <summary>
        /// Register all your interfaces and implementations here
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IFoodAndDrugEnforcementService, FoodAndDrugEnforcementService>();

            return services;
        }
    }
}
