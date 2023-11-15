using Microsoft.AspNetCore.Mvc;
using ScaniaTest.Vehicles.Dtos;
using ScaniaTest.Vehicles.Services;

namespace ScaniaTest.Vehicles.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAll()
        {
            var vehicles = await _vehicleService.GetVehiclesAsync();
            if (!vehicles.Any()) return NotFound();
            return Ok(vehicles);
        }

        [HttpGet("ByCustomer/{id:int}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAllByCustomerId(int id)
        {
            var vehicles= await _vehicleService.GetVehiclesByCustomerAsync(id);
            if (!vehicles.Any()) return NotFound();
            return Ok(vehicles );
        }

         [HttpGet("ByStatus/{status}")]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetAllByStatusId(bool status)
        {
            var vehicles= await _vehicleService.GetVehiclesByStatusAsync(status);
            if (!vehicles.Any()) return NotFound();
            return Ok( vehicles);
        }


      
    }
}
