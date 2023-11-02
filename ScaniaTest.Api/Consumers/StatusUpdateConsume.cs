using MassTransit;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ScaniaTest.Common;
using ScaniaTest.Common.Data;
using ScaniaTest.Common.Entities;
using ScaniaTest.Common.Repositories;

namespace ScaniaTest.Api.Consumers
{
    public class StatusUpdateConsume : IConsumer<VehicleStatus>
    {
        private readonly IRepository<DataContext, Vehicle> repository;
        public StatusUpdateConsume(IRepository<DataContext, Vehicle> repository)
        {
            this.repository = repository;
        }
        public async Task Consume(ConsumeContext<VehicleStatus> context)
        {
            var message = context.Message;
            var vehicles = await repository.GetAllAsync();

            var vehicle = vehicles.Where(v=> v.Id==message.Id).FirstOrDefault();

            vehicle.Status=message.Status;

            await repository.UpdateAsync(vehicle);
        }
    }
}
