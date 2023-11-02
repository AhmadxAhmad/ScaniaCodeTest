

using ScaniaTest.Common.Entities;

namespace ScaniaTest.Customers.Dtos
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public IEnumerable<Vehicle> Vehicles { get; set; }
     
    }
}