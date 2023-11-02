using ScaniaTest.Common.Data;
using ScaniaTest.Common.Entities;
using ScaniaTest.Common.Repositories;
using ScaniaTest.Customers.Dtos;

namespace ScaniaTest.Customers.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<DataContext, Customer> repository;
        public CustomerService(IRepository<DataContext, Customer> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            return (await repository.GetAllAsync(x=> x.Vehicles))
                        .Select(v => v.AsDto());
        }

        public async Task<Customer> GetByCustomerId(int id)
        {
           return  await repository.GetOneBy(c => c.Id == id);
        }

    }
}