using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodingAssessment.Infrastructure;
using System.Linq;

namespace WebApi.EndToEndTests
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder            
                 .ConfigureServices(services =>
                 {
                     var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(DbContextOptions<CodingAssessment.Infrastructure.DbContext>));

                     services.Remove(descriptor);

                 })
                 .ConfigureTestServices(testServices =>
                 {
                     testServices.AddDbContext<CodingAssessment.Infrastructure.DbContext>(options =>
                     {
                         options.UseInMemoryDatabase("inmemdb", b => b.EnableNullChecks(false));
                         options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                         options.EnableSensitiveDataLogging();
                         options.EnableDetailedErrors();
                     });
                 });
        }
    }

}