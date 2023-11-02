using ScaniaTest.Common.Data;
using ScaniaTest.Common.Entities;
using ScaniaTest.Common.Repositories;
using ScaniaTest.Vehicles.Dtos;

namespace ScaniaTest.Vehicles.Services
{
    public class VehicleService:IVehicleService
    {

        private readonly IRepository<DataContext,Vehicle> repository;
        public VehicleService(IRepository<DataContext, Vehicle> repository )
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesAsync()
        {
            var Vehicles = await repository.GetAllAsync(i=> i.Customer);
            return Vehicles.Select(v=>v.AsDto());
        }
        public async Task<IEnumerable<VehicleDto>> GetVehiclesByCustomerAsync(int customerId)
        {
            return (await repository.GetAllByAsync(v=>v.CustomerId==customerId,i=>i.Customer))
                        .Select(v=>v.AsDto());
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByStatusAsync(bool status)
        {
            return (await repository.GetAllByAsync(v=>v.Status==status, i => i.Customer))
                        .Select(v=>v.AsDto());
        }
    }
}