using ScaniaTest.Common.Entities;
using ScaniaTest.Common;
using ScaniaTest.Simulator.Services;
using ScaniaTest.Common.Data;
using ScaniaTest.Middleware;

var builder = WebApplication.CreateBuilder(args);



builder.Services.ConfigureStartUp();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(configuration: builder.Configuration);

builder.Services.AddSerilogLogger(configuration: builder.Configuration);

builder.Services.AddSqlDb<MQDataContext>(ConnectionString: builder.Configuration.GetConnectionString("Connection"), isDev: true);

builder.Services.AddSpecificsCors(origins: builder.Configuration.GetSection("Origins").Get<string[]>());

builder.Services.AddRepository<MQDataContext, VehicleStatus>();

builder.Services.AddMassTransitWithRabbitMQ(configuration: builder.Configuration);

builder.Services.AddScoped<IVehicleStatusService, VehicleStatusService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAALoggingMiddleware();

app.UseCors();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
