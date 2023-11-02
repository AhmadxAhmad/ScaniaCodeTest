using Microsoft.EntityFrameworkCore;
using ScaniaTest.Common.Entities;

namespace ScaniaTest.Common.Data
{
    public partial class MQDataContext : DbContext
    {
        public MQDataContext(DbContextOptions<MQDataContext> op) :base (op)
        {
            Database.EnsureCreated();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<VehicleStatus>()
                        .ToTable("VehicleStatus");

            modelBuilder.Entity<VehicleStatus>().HasData(
                    new VehicleStatus { Id = "YS2R4X20005399401", Status = true },
                    new VehicleStatus { Id = "VLUR4X20009093588", Status = true },
                    new VehicleStatus { Id = "VLUR4X20009048066", Status = false },
                    new VehicleStatus { Id = "YS2R4X20005388011", Status = true },
                    new VehicleStatus { Id = "YS2R4X20005387949", Status = false },
                    new VehicleStatus { Id = "YS2R4X20005387765", Status = true },
                    new VehicleStatus { Id = "YS2R4X20005387055", Status = false }
            );


        }
    }
}