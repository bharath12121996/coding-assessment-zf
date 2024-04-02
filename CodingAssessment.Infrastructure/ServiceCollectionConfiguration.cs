using CodingAssessment.Domain.Interfaces;
using CodingAssessment.Domain.Model;
using CodingAssessment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CodingAssessment.Infrastructure
{
    public static class ServiceCollectionConfiguration
    {
        /// <summary>
        /// Register all your Interfaces 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFoodAndDrugEnforcementRepository, FoodAndDrugEnforcementRepository>();
            services.AddScoped<ISmtpClientRepository, SmtpClientRepository>();

            services.AddDbContext<DbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: configuration.GetSection("InMemoryDatabase").Value, b => b.EnableNullChecks(false));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
            });
            return services;
        }
    }
}
