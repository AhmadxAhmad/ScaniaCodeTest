using Microsoft.AspNetCore.Mvc;
using Moq;
using ScaniaTest.Common.Entities;
using ScaniaTest.Common.Repositories;
using ScaniaTest.Vehicles.Controllers;
using ScaniaTest.Vehicles.Dtos;
using ScaniaTest.Vehicles.Services;

namespace ScaniaTest.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task DataFromService()
        {
            var mockRepository = new Mock<IRepository<Vehicle>>();
            mockRepository.Setup(r => r.GetAllByAsync(x=>x.CustomerId==3))
                .ReturnsAsync(new List<Vehicle>());

            var vehicleService = new VehicleService(mockRepository.Object);

            var result = await vehicleService.GetVehiclesByCustomerAsync(3);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<VehicleDto>));

            Assert.AreEqual(new List<Vehicle>().Count, result.Count());

        }
    }
}