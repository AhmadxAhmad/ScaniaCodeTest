
using Microsoft.AspNetCore.Mvc;
using ScaniaTest.Customers.Dtos;
using ScaniaTest.Customers.Services;

namespace ScaniaTest.Customers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            return Ok( await _customerService.GetCustomersAsync());
        }

      
    }
}
