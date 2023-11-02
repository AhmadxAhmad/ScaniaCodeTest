using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ScaniaTest.Simulator.Services;

namespace ScaniaTest.Simulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleSimulatorController : ControllerBase
    {
        private readonly IVehicleStatusService _vehicleService;
        public VehicleSimulatorController(IVehicleStatusService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task Update()
        {
            await _vehicleService.UpdateStatus();
        }
     }
}
