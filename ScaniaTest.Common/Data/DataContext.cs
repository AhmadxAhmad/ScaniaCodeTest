using Microsoft.EntityFrameworkCore;
using ScaniaTest.Common.Entities;

namespace ScaniaTest.Common.Data
{
    public partial class DataContext  : DbContext
    {
        public DataContext(DbContextOptions<DataContext> op) :base (op)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>()
                         .ToTable("Vehicles");
            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = "YS2R4X20005399401", RegNr = "ABC123", CustomerId = 1, Status = true },
                new Vehicle { Id = "VLUR4X20009093588", RegNr = "DEF456", CustomerId = 1, Status = true },
                new Vehicle { Id = "VLUR4X20009048066", RegNr = "GHI789", CustomerId = 1, Status = false },
                new Vehicle { Id = "YS2R4X20005388011", RegNr = "JKL012", CustomerId = 2, Status = true },
                new Vehicle { Id = "YS2R4X20005387949", RegNr = "MNO345", CustomerId = 2, Status = false },
                new Vehicle { Id = "YS2R4X20005387765", RegNr = "PQR678", CustomerId = 3, Status = true },
                new Vehicle { Id = "YS2R4X20005387055", RegNr = "STU901", CustomerId = 3, Status = false }
            );
            modelBuilder.Entity<Customer>()
                        .ToTable("Customers");
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Name = "Kalles Grustransporter AB", Address = "Cementvägen 8, 111 11 Södertälje" },
                new Customer { Id = 2, Name = "Johans Bulk AB", Address = "Balkvägen 12, 222 22 Stockholm" },
                new Customer { Id = 3, Name = "Haralds Värdetransporter AB", Address = "Budgetvägen 1, 333 33 Uppsala " }

            );



        }
    }
}