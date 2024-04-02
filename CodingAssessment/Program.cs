using CodingAssessment.Application;
using CodingAssessment.Infrastructure;
using Serilog;
using FluentValidation.AspNetCore;
using CodingAssessment.Filters;
using CodingAssessment.Domain.Model;

Log.Logger = new LoggerConfiguration()
.MinimumLevel.Information()
.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Host.UseSerilog();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
//Registering Application Layer Dependencies
builder.Services.AddScoped<ExceptionFilter>();
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.Configure<Settings>(builder.Configuration.GetSection("DataService"));

//Registering Infrastructure Layer Dependencies
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PagedRequestFluentValidator>());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOptions();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DbContext>();
        await DbContextDataSeed.SeedSampleDataAsync(context,builder.Configuration.GetValue<string>("BaseApiEndpoint"));
    }

    app.UseDeveloperExceptionPage();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
