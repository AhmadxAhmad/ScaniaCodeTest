using ScaniaTest.Customers.Dtos;

namespace ScaniaTest.Customers.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
    }
}