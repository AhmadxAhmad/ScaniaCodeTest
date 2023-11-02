using ScaniaTest.Common.Entities;
using ScaniaTest.Customers.Dtos;

namespace ScaniaTest.Customers
{
    public static class Extensions
 {
        public static CustomerDto AsDto(this Customer customer)
        {
            return new CustomerDto() {
                Name = customer.Name,
                Address = customer.Address
                , Vehicles = customer.Vehicles
            };
        }    

      
    }
}

