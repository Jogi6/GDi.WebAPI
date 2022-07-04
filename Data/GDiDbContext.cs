using GDi.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GDi.WebAPI.Data
{
    public class GDiDbContext : DbContext
    {
        public GDiDbContext(DbContextOptions options) : base(options)
        {
        }

        //Dbset
        
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleLocation> VehicleLocations { get; set; }

    }
}
