using ScaniaTest.Vehicles.Dtos;


namespace ScaniaTest.Vehicles.Services
{
    public interface IVehicleService
    {
        Task<IEnumerable<VehicleDto>> GetVehiclesAsync();
        Task<IEnumerable<VehicleDto>> GetVehiclesByCustomerAsync(int customerId);
        Task<IEnumerable<VehicleDto>> GetVehiclesByStatusAsync(bool status);
    }
}