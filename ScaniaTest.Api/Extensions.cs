using ScaniaTest.Common.Entities;
using ScaniaTest.Vehicles.Dtos;

namespace ScaniaTest.Vehicles
{
    public static class Extensions
 {
        public static VehicleDto AsDto(this Vehicle vehicle)
        {
            return new VehicleDto() {
                Id=vehicle.Id,
                RegNr=vehicle.RegNr,
                CustomerName=vehicle.Customer.Name,
                Status=vehicle.Status
            };
        }    

      
    }
}

