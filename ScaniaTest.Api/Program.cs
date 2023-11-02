using ScaniaTest.Common;
using ScaniaTest.Common.Data;
using ScaniaTest.Common.Entities;
using ScaniaTest.Middleware;
using ScaniaTest.Vehicles.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureStartUp();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(configuration: builder.Configuration);

builder.Services.AddSerilogLogger(configuration: builder.Configuration);

builder.Services.AddSqlDb<DataContext>(ConnectionString: builder.Configuration.GetConnectionString("Connection"), isDev: true);

builder.Services.AddSpecificsCors(origins: builder.Configuration.GetSection("Origins").Get<string[]>());

builder.Services.AddMassTransitWithRabbitMQ(configuration: builder.Configuration);

builder.Services.AddRepository<DataContext, Vehicle>();

builder.Services.AddScoped<IVehicleService, VehicleService>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAALoggingMiddleware();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
