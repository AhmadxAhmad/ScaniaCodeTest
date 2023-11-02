using MassTransit;
using ScaniaTest.Common;
using ScaniaTest.Common.Data;
using ScaniaTest.Common.Repositories;

namespace ScaniaTest.Simulator.Services
{
    public class VehicleStatusService : IVehicleStatusService
    {

        private readonly IRepository<MQDataContext, VehicleStatus> repository;

        private readonly IPublishEndpoint publishedEndpoint;
        public VehicleStatusService(IRepository<MQDataContext, VehicleStatus> repository, IPublishEndpoint publishedEndpoint)
        {
            this.repository = repository;
            this.publishedEndpoint = publishedEndpoint;
        }

        public async Task UpdateStatus()
        {
            var random = new Random();

            var VehicleStatuses = (await repository.GetAllAsync()).ToList();
           
            var randV= VehicleStatuses[random.Next(VehicleStatuses.Count)];
            randV.Status = random.Next(2) == 0;

            await repository.UpdateAsync(randV);

            await publishedEndpoint.Publish(randV);

        }

    }
}